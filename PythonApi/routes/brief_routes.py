import asyncio
from datetime import date
from fastapi import APIRouter, HTTPException
from models.brief_models import BriefGenerateRequest, OnboardingRequest, BriefResponse
from agents import analyst, researcher, synthesizer, judge
from services.news_fetcher import fetch_top_stories
from services.history_service import get_history_bite
from services.formatter import format_whatsapp, format_email_html

router = APIRouter()

ONBOARDING_NEWS = [
    "AI traces its roots to Alan Turing's 1950 paper asking 'Can machines think?'",
    "The 2017 Transformer paper 'Attention Is All You Need' underpins every modern LLM.",
    "GPT-3 showed that scaling alone unlocked emergent capabilities in language models.",
    "ChatGPT reached 100 million users in two months — fastest consumer adoption ever.",
    "Today's AI landscape spans reasoning models, multimodal systems, and autonomous agents.",
]


@router.post("/brief/generate", response_model=BriefResponse)
async def generate_brief(request: BriefGenerateRequest) -> BriefResponse:
    try:
        news_items = await fetch_top_stories(5)
        history_bite = get_history_bite(request.history_bite_index)

        analyst_out, researcher_out, synthesizer_out = await asyncio.gather(
            analyst.run(news_items),
            researcher.run(news_items),
            synthesizer.run(news_items),
        )

        judge_out = await judge.run(
            analyst=analyst_out,
            researcher=researcher_out,
            synthesizer=synthesizer_out,
            history_bite=history_bite,
            is_onboarding=False
        )

        today = date.today()
        brief_html = format_email_html(judge_out.final_brief, judge_out.tldr, history_bite, today)
        whatsapp_text = judge_out.whatsapp_text or format_whatsapp(judge_out.tldr, today, history_bite)

        return BriefResponse(
            brief_html=brief_html,
            whatsapp_text=whatsapp_text,
            tldr=judge_out.tldr
        )
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))


@router.post("/brief/onboarding", response_model=BriefResponse)
async def generate_onboarding(request: OnboardingRequest) -> BriefResponse:
    try:
        history_bite = get_history_bite(1)

        analyst_out, researcher_out, synthesizer_out = await asyncio.gather(
            analyst.run(ONBOARDING_NEWS),
            researcher.run(ONBOARDING_NEWS),
            synthesizer.run(ONBOARDING_NEWS),
        )

        judge_out = await judge.run(
            analyst=analyst_out,
            researcher=researcher_out,
            synthesizer=synthesizer_out,
            history_bite=history_bite,
            is_onboarding=True
        )

        today = date.today()
        brief_html = format_email_html(judge_out.final_brief, judge_out.tldr, history_bite, today)
        whatsapp_text = judge_out.whatsapp_text or format_whatsapp(judge_out.tldr, today, history_bite)

        return BriefResponse(
            brief_html=brief_html,
            whatsapp_text=whatsapp_text,
            tldr=judge_out.tldr
        )
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))
