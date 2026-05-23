HISTORY_BITES = [
    "In 1950, Alan Turing asked 'Can machines think?' and proposed what became the Turing Test — if a machine could hold a conversation indistinguishable from a human, it could be considered intelligent.",
    "The Dartmouth Conference of 1956 coined the term 'artificial intelligence.' Researchers believed every aspect of human intelligence could be simulated by a machine — an optimism that would drive and haunt the field for decades.",
    "The First AI Winter hit in the early 1970s. AI had promised too much and delivered too little. Funding dried up and skepticism set in — a pattern that would repeat itself.",
    "In the 1980s, expert systems encoded human knowledge as rules to solve domain problems. Companies invested billions, but they were brittle — they couldn't learn or generalize, and collapsed under their own weight.",
    "The Second AI Winter arrived in the late 1980s when expert systems failed at scale. The LISP machine market evaporated, DARPA slashed funding, and 'AI' became a dirty word in research circles.",
    "Geoffrey Hinton's 1986 backpropagation paper gave neural networks a way to learn. By the 1990s, Yann LeCun used it to read handwritten ZIP codes for the US Postal Service — the first real-world deep learning deployment.",
    "In 2012, AlexNet won the ImageNet competition by a margin that shocked the computer vision world, cutting error rates nearly in half. Deep learning was no longer a curiosity — it was the future.",
    "Google's 2017 paper 'Attention Is All You Need' introduced the Transformer. By replacing recurrence with self-attention, it could train in parallel at massive scale. Every modern LLM — GPT, Claude, Gemini — descends from this single paper.",
    "GPT-3 in 2020 showed that scale alone unlocked remarkable capability. ChatGPT in November 2022 hit 100 million users in two months — the fastest consumer product adoption in history. The AI era had arrived.",
]


def get_history_bite(index: int) -> str:
    if not HISTORY_BITES:
        return ""
    return HISTORY_BITES[(index - 1) % len(HISTORY_BITES)]
