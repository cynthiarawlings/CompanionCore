export interface ConversationMessage {
    id: string;
    role: string;
    content: string;
    timestamp: string;
}

export interface Conversation {
    id: string;
    companionId: string;
    startedAt: string;
    lastMessageAt?: string;
    title?: string;
    messages: ConversationMessage[];
}