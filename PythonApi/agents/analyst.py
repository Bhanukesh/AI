import os
import anthropic

_client: anthropic.AsyncAnthropic | None = None


def get_client() -> anthropic.AsyncAnthropic:
    global _client
    if _client is None:
        _client = anthropic.AsyncAnthropic(api_key=os.environ["ANTHROPIC_API_KEY"])
    return _client


SYSTEM_PROMPT = """You are the Analyst agent in an AI news council.
Your role: deep reasoning, historical accuracy, and implications.
Write for someone who wants to understand the 'why' behind today's AI news, not just consume headlines.
Connect today's stories to the broader arc of AI history.
Be clear, insightful, and substantive. 3-4 paragraphs."""


async def run(news_items: list[str]) -> str:
    news_text = "\n".join(f"- {item}" for item in news_items)
    message = await get_client().messages.create(
        model="claude-haiku-4-5-20251001",
        max_tokens=1024,
        system=SYSTEM_PROMPT,
        messages=[
            {
                "role": "user",
                "content": f"Today's AI news:\n{news_text}\n\nWrite your analysis."
            }
        ]
    )
    return message.content[0].text
