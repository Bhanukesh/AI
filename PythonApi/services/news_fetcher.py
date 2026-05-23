import asyncio
import feedparser
from datetime import datetime

RSS_FEEDS = [
    "https://www.technologyreview.com/feed/",
    "https://techcrunch.com/tag/artificial-intelligence/feed/",
    "https://venturebeat.com/category/ai/feed/",
]

FALLBACK_STORIES = [
    "OpenAI releases new capabilities for GPT-4o with improved reasoning and multimodal understanding.",
    "Google DeepMind publishes research on AI systems that can learn from fewer examples.",
    "Anthropic releases safety research on scalable oversight for advanced AI systems.",
    "Meta open-sources new LLM with competitive performance at reduced compute costs.",
    "AI adoption in enterprise software reaches record levels according to new industry report.",
]


def _parse_feed(url: str, n: int) -> list[str]:
    try:
        feed = feedparser.parse(url)
        stories = []
        for entry in feed.entries[:n]:
            title = entry.get("title", "")
            summary = entry.get("summary", entry.get("description", ""))
            # Strip HTML tags from summary
            import re
            summary = re.sub(r"<[^>]+>", "", summary)[:300]
            if title:
                stories.append(f"{title}: {summary}".strip(": "))
        return stories
    except Exception:
        return []


async def fetch_top_stories(n: int = 5) -> list[str]:
    loop = asyncio.get_event_loop()
    all_stories: list[str] = []

    for feed_url in RSS_FEEDS:
        stories = await loop.run_in_executor(None, _parse_feed, feed_url, n)
        all_stories.extend(stories)
        if len(all_stories) >= n:
            break

    if not all_stories:
        return FALLBACK_STORIES[:n]

    return all_stories[:n]
