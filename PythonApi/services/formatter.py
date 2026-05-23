from datetime import date


def format_whatsapp(tldr: list[str], today: date, history_bite: str) -> str:
    date_str = today.strftime("%b %d, %Y")
    bullets = "\n".join(f"• {line}" for line in tldr[:3])
    bite = history_bite[:80] + "..." if len(history_bite) > 80 else history_bite
    msg = f"🤖 AI Daily Brief | {date_str}\n{bullets}\n📚 {bite}\n\nReply FULL for the complete brief."
    return msg[:300]


def format_email_html(brief_text: str, tldr: list[str], history_bite: str, today: date) -> str:
    date_str = today.strftime("%B %d, %Y")
    bullets_html = "".join(f"<li>{line}</li>" for line in tldr)
    brief_paragraphs = "".join(f"<p>{p.strip()}</p>" for p in brief_text.split("\n\n") if p.strip())

    return f"""<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>AI Daily Brief | {date_str}</title>
<style>
  body {{ font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif; background: #f4f4f5; margin: 0; padding: 20px; }}
  .container {{ max-width: 640px; margin: 0 auto; background: #fff; border-radius: 12px; overflow: hidden; box-shadow: 0 2px 8px rgba(0,0,0,.08); }}
  .header {{ background: linear-gradient(135deg, #1e1b4b 0%, #312e81 100%); color: #fff; padding: 32px 24px; text-align: center; }}
  .header h1 {{ margin: 0; font-size: 22px; }}
  .header p {{ margin: 6px 0 0; opacity: .75; font-size: 14px; }}
  .section {{ padding: 24px; border-bottom: 1px solid #f0f0f0; }}
  .section h2 {{ margin: 0 0 12px; font-size: 16px; color: #312e81; text-transform: uppercase; letter-spacing: .05em; }}
  .section p {{ margin: 0 0 10px; line-height: 1.7; color: #374151; }}
  .tldr ul {{ margin: 0; padding-left: 20px; }}
  .tldr li {{ margin-bottom: 8px; line-height: 1.6; color: #374151; }}
  .history {{ background: #fafaf9; }}
  .history blockquote {{ margin: 0; padding: 16px; border-left: 4px solid #312e81; color: #4b5563; font-style: italic; line-height: 1.7; }}
  .footer {{ padding: 20px 24px; text-align: center; color: #9ca3af; font-size: 12px; }}
  .footer a {{ color: #6366f1; text-decoration: none; }}
  .badge {{ display: inline-block; background: #312e81; color: #fff; font-size: 11px; padding: 2px 8px; border-radius: 99px; margin-bottom: 8px; }}
</style>
</head>
<body>
<div class="container">
  <div class="header">
    <span class="badge">Powered by LLM Council</span>
    <h1>🤖 AI Daily Brief</h1>
    <p>{date_str}</p>
  </div>

  <div class="section">
    <h2>Today's Brief</h2>
    {brief_paragraphs}
  </div>

  <div class="section tldr">
    <h2>⚡ TLDR</h2>
    <ul>{bullets_html}</ul>
  </div>

  <div class="section history">
    <h2>📚 AI History Bite</h2>
    <blockquote>{history_bite}</blockquote>
  </div>

  <div class="footer">
    <p>Delivered by LLM Council &mdash; Claude · GPT-4o · Gemini · Grok</p>
    <p><a href="#">Unsubscribe</a></p>
  </div>
</div>
</body>
</html>"""
