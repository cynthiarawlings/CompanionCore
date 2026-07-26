import type { ConversationSummary } from "../models/ConversationSummary";

interface Props {
    conversations: ConversationSummary[];
    selectedConversationId?: string;
    onSelectConversation: (id: string) => void;
    onNewChat: () => void;
}

export default function CompanionChatList({
    conversations,
    selectedConversationId,
    onSelectConversation,
    onNewChat
}: Props) {

    return (
        <div
            style={{
                width: "260px",
                borderRight: "1px solid #ccc",
                padding: "15px",
                height: "100%",
                boxSizing: "border-box"
            }}
        >

            <button
                onClick={onNewChat}
                style={{
                    width: "100%",
                    padding: "10px",
                    marginBottom: "15px"
                }}
            >
                + New Chat
            </button>


            <h3>
                Chats
            </h3>


            {conversations.length === 0 && (

                <p>
                    No conversations yet.
                </p>

            )}


            {conversations.map(conversation => (

                <button
                    key={conversation.id}
                    onClick={() =>
                        onSelectConversation(conversation.id)
                    }
                    style={{
                        width: "100%",
                        textAlign: "left",
                        padding: "10px",
                        marginBottom: "8px",
                        cursor: "pointer",

                        background:
                            selectedConversationId === conversation.id
                                ? "#e6e6e6"
                                : "transparent",

                        border: "1px solid #ddd",
                        borderRadius: "6px"
                    }}
                >

                    {conversation.title
                        ?? "New Conversation"}

                </button>

            ))}

        </div>
    );
}