import os
from openai import AsyncOpenAI

_client: AsyncOpenAI | None = None

SYSTEM_PROMPT = (
    "You are the Synthesizer agent in an AI news council. "
    "Your role: cross-source synthesis and trend spotting. "
    "Find the patterns across today's headlines. Identify the bigger story behind the news. "
    "Make connections the other analysts might miss. What does today tell us about where AI is heading? "
    "Be visionary but grounded. 3-4 paragraphs."
)


def get_client() -> AsyncOpenAI:
    global _client
    if _client is None:
        _client = AsyncOpenAI(
            api_key=os.environ["GEMINI_API_KEY"],
            base_url="https://generativelanguage.googleapis.com/v1beta/openai/"
        )
    return _client


async def run(news_items: list[str]) -> str:
    news_text = "\n".join(f"- {item}" for item in news_items)
    response = await get_client().chat.completions.create(
        model="gemini-3.1-flash-lite",
        messages=[
            {"role": "system", "content": SYSTEM_PROMPT},
            {"role": "user", "content": f"Today's AI news:\n{news_text}\n\nWrite your synthesis."}
        ],
        max_tokens=1024,
    )
    return response.choices[0].message.content
