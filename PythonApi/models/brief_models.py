from pydantic import BaseModel
from uuid import UUID


class BriefGenerateRequest(BaseModel):
    user_id: UUID
    history_bite_index: int = 0


class OnboardingRequest(BaseModel):
    user_id: UUID


class AgentScore(BaseModel):
    accuracy: int
    clarity: int
    depth: int
    relevance: int
    total: int


class JudgeOutput(BaseModel):
    final_brief: str
    tldr: list[str]
    whatsapp_text: str
    scores: dict[str, AgentScore]
    winner: str


class BriefResponse(BaseModel):
    brief_html: str
    whatsapp_text: str
    tldr: list[str]
