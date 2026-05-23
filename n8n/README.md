# N8N Workflows — LLM Council

Two importable workflow files for the AI Daily Brief system.

## Setup

### 1. Install N8N locally (no account needed)
```bash
npm install -g n8n
n8n start
```
Opens at **http://localhost:5678**

### 2. Set Environment Variables in N8N
In N8N → Settings → Variables, add:

| Variable | Value |
|---|---|
| `API_BASE_URL` | `http://localhost:PORT` (your ApiService port from Aspire) |
| `RESEND_API_KEY` | Your Resend API key |
| `TWILIO_WHATSAPP_NUMBER` | `whatsapp:+14155238886` (Twilio sandbox) |

### 3. Add Twilio Credentials
In N8N → Credentials → New → Twilio:
- Account SID: from your Twilio console
- Auth Token: from your Twilio console

### 4. Import the Workflows

#### Daily Brief Workflow (`daily-brief-workflow.json`)
- Runs every day at **8PM** via cron
- Fetches all users → generates brief via LLM Council → sends WhatsApp + Email → logs delivery

**To import:** N8N → Workflows → Import from File → select `workflows/daily-brief-workflow.json`

#### WhatsApp Reply Listener (`whatsapp-reply-workflow.json`)
- Triggered when a user replies **FULL** to the WhatsApp message
- Sends the full HTML email via Resend

**To import:** N8N → Workflows → Import from File → select `workflows/whatsapp-reply-workflow.json`

**Twilio webhook URL** (set this in your Twilio WhatsApp sandbox settings):
```
http://YOUR_PUBLIC_URL/webhook/twilio-webhook
```
Use [ngrok](https://ngrok.com) to expose localhost: `ngrok http 5678`

---

## Testing Without Waiting for 8PM

In N8N, open the Daily Brief workflow → click **Execute Workflow** to trigger it manually.
