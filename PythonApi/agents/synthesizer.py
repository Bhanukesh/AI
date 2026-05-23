import os
import google.generativeai as genai

_model = None


def get_model():
    global _model
    if _model is None:
        genai.configure(api_key=os.environ["GEMINI_API_KEY"])
        _model = genai.GenerativeModel(
            model_name="gemini-1.5-pro",
            system_instruction=(
                "You are the Synthesizer agent in an AI news council. "
                "Your role: cross-source synthesis and trend spotting. "
                "Find the patterns across today's headlines. Identify the bigger story behind the news. "
                "Make connections the other analysts might miss. What does today tell us about where AI is heading? "
                "Be visionary but grounded. 3-4 paragraphs."
            )
        )
    return _model


async def run(news_items: list[str]) -> str:
    news_text = "\n".join(f"- {item}" for item in news_items)
    model = get_model()
    response = await model.generate_content_async(
        f"Today's AI news:\n{news_text}\n\nWrite your synthesis."
    )
    return response.text
