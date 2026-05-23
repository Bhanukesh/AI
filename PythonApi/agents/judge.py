import os
import json
from openai import AsyncOpenAI
from models.brief_models import JudgeOutput, AgentScore

_client: AsyncOpenAI | None = None


def get_client() -> AsyncOpenAI:
    global _client
    if _client is None:
        _client = AsyncOpenAI(
            api_key=os.environ["GROK_API_KEY"],
            base_url="https://api.x.ai/v1"
        )
    return _client


SYSTEM_PROMPT = """You are the Judge in an AI news council. You receive three agent outputs and must:
1. Score each on: Accuracy (1-10), Clarity (1-10), Depth (1-10), Relevance (1-10)
2. Pick the winner (highest total score)
3. Merge the best insights from all three into a final brief
4. Write exactly 3 TLDR bullet points (max 2 lines each)
5. Write a WhatsApp-friendly summary under 200 characters with emojis

Respond ONLY with valid JSON in this exact structure:
{
  "scores": {
    "analyst": {"accuracy": 0, "clarity": 0, "depth": 0, "relevance": 0, "total": 0},
    "researcher": {"accuracy": 0, "clarity": 0, "depth": 0, "relevance": 0, "total": 0},
    "synthesizer": {"accuracy": 0, "clarity": 0, "depth": 0, "relevance": 0, "total": 0}
  },
  "winner": "analyst|researcher|synthesizer",
  "final_brief": "merged final text here",
  "tldr": ["bullet 1", "bullet 2", "bullet 3"],
  "whatsapp_text": "short punchy text under 200 chars with emojis"
}"""


async def run(
    analyst: str,
    researcher: str,
    synthesizer: str,
    history_bite: str,
    is_onboarding: bool = False
) -> JudgeOutput:
    user_content = f"""ANALYST OUTPUT:
{analyst}

RESEARCHER OUTPUT:
{researcher}

SYNTHESIZER OUTPUT:
{synthesizer}

HISTORY BITE FOR TODAY:
{history_bite}

{"This is an ONBOARDING brief for a new user. Make the final brief welcoming and educational." if is_onboarding else "This is a daily news brief for a returning user."}

Score, pick a winner, merge the best parts, and produce the final brief + TLDR."""

    response = await get_client().chat.completions.create(
        model="grok-4.3",
        messages=[
            {"role": "system", "content": SYSTEM_PROMPT},
            {"role": "user", "content": user_content}
        ],
        max_tokens=2048,
        response_format={"type": "json_object"}
    )

    raw = response.choices[0].message.content or "{}"
    data = json.loads(raw)

    scores = {
        agent: AgentScore(
            accuracy=s["accuracy"],
            clarity=s["clarity"],
            depth=s["depth"],
            relevance=s["relevance"],
            total=s["total"]
        )
        for agent, s in data.get("scores", {}).items()
    }

    return JudgeOutput(
        final_brief=data.get("final_brief", ""),
        tldr=data.get("tldr", []),
        whatsapp_text=data.get("whatsapp_text", ""),
        scores=scores,
        winner=data.get("winner", "unknown")
    )
