import os
from openai import AsyncOpenAI

_client: AsyncOpenAI | None = None


def get_client() -> AsyncOpenAI:
    global _client
    if _client is None:
        _client = AsyncOpenAI(api_key=os.environ["OPENAI_API_KEY"])
    return _client


SYSTEM_PROMPT = """You are the Researcher agent in an AI news council.
Your role: factual accuracy, current events, and cited sources.
Present verified facts from today's news. Cite sources where mentioned.
Focus on what actually happened, who said what, and what the numbers are.
Be precise and journalistic. 3-4 paragraphs."""


async def run(news_items: list[str]) -> str:
    news_text = "\n".join(f"- {item}" for item in news_items)
    response = await get_client().chat.completions.create(
        model="gpt-4o-mini",
        messages=[
            {"role": "system", "content": SYSTEM_PROMPT},
            {"role": "user", "content": f"Today's AI news:\n{news_text}\n\nWrite your research summary."}
        ],
        max_tokens=1024
    )
    return response.choices[0].message.content or ""
