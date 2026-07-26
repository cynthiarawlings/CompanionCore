import type { ChatRequest } from "../models/ChatRequest";
import type { ChatResponse } from "../models/ChatResponse";
import type { Conversation } from "../models/Conversation";

const CHAT_API = "https://localhost:7266/api/chat";
const CONVERSATION_API = "https://localhost:7266/api/conversations";

export async function sendMessage(
    request: ChatRequest
): Promise<ChatResponse> {

    const response = await fetch(CHAT_API, {
        method: "POST",

        headers: {
            "Content-Type": "application/json"
        },

        body: JSON.stringify(request)
    });

    if (!response.ok) {
        throw new Error("Failed to send message.");
    }

    return await response.json();
}

export async function getConversation(
    id: string
): Promise<Conversation> {

    const response = await fetch(
        `${CONVERSATION_API}/${id}`
    );

    if (!response.ok) {
        throw new Error("Failed to load conversation.");
    }

    return await response.json();
}