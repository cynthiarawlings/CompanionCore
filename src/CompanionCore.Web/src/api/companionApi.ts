import type { Companion } from "../models/Companion";
import type { ConversationSummary } from "../models/ConversationSummary";

const API_URL = "https://localhost:7266/api/companions";

export async function getCompanions(): Promise<Companion[]> {

    const response = await fetch(API_URL);

    if (!response.ok) {
        throw new Error("Failed to load companions");
    }

    return await response.json();
}

export async function getCompanion(
    id: string
): Promise<Companion> {

    const response = await fetch(
        `${API_URL}/${id}`
    );

    if (!response.ok) {
        throw new Error("Failed to load companion");
    }

    return await response.json();
}

export async function getCompanionConversations(
    companionId: string
): Promise<ConversationSummary[]> {

    const response = await fetch(
        `${API_URL}/${companionId}/conversations`
    );

    if (!response.ok) {
        throw new Error("Failed to load conversations");
    }

    return await response.json();
}

export async function createCompanion(
    companion: Omit<Companion, "id">
): Promise<Companion> {

    const response = await fetch(API_URL, {
        method: "POST",

        headers: {
            "Content-Type": "application/json"
        },

        body: JSON.stringify(companion)
    });

    if (!response.ok) {
        throw new Error("Failed to create companion");
    }

    return await response.json();
}

export async function updateCompanion(
    id: string,
    companion: Omit<Companion, "id">
): Promise<Companion> {

    const response = await fetch(
        `${API_URL}/${id}`,
        {
            method: "PUT",

            headers: {
                "Content-Type": "application/json"
            },

            body: JSON.stringify(companion)
        }
    );

    if (!response.ok) {
        throw new Error("Failed to update companion");
    }

    return await response.json();
}

export async function deleteCompanion(
    id: string
): Promise<void> {

    const response = await fetch(`${API_URL}/${id}`, {
        method: "DELETE"
    });

    if (!response.ok) {
        throw new Error("Failed to delete companion");
    }
}