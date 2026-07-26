import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";

import {
    sendMessage,
    getConversation
} from "../api/chatApi";

import {
    getCompanion,
    getCompanionConversations
} from "../api/companionApi";

import CompanionChatList from "../components/CompanionChatList";

import type { Companion } from "../models/Companion";
import type { Conversation } from "../models/Conversation";
import type { ConversationSummary } from "../models/ConversationSummary";


export default function ChatPage() {

    const { id } = useParams();

    const [companion, setCompanion] =
        useState<Companion | null>(null);

    const [conversation, setConversation] =
        useState<Conversation | null>(null);

    const [conversations, setConversations] =
        useState<ConversationSummary[]>([]);

    const [conversationId, setConversationId] =
        useState<string>();

    const [message, setMessage] =
        useState("");

    const [loading, setLoading] =
        useState(false);


    useEffect(() => {

        async function loadData() {

            if (!id) {
                return;
            }

            const companionData =
                await getCompanion(id);

            setCompanion(companionData);


            const conversationData =
                await getCompanionConversations(id);

            setConversations(conversationData);

        }


        loadData();

    }, [id]);


    async function loadConversation(
        id: string
    ) {

        const data =
            await getConversation(id);

        setConversation(data);

        setConversationId(data.id);

    }


    function startNewChat() {

        setConversation(null);

        setConversationId(undefined);

    }


    async function handleSend() {

        if (!message.trim() || !id) {
            return;
        }

        setLoading(true);

        try {

            const response = await sendMessage({

                companionId: id,

                conversationId,

                message

            });


            setConversationId(
                response.conversationId
            );


            const updatedConversation =
                await getConversation(
                    response.conversationId
                );


            setConversation(updatedConversation);


            // Refresh sidebar so new chats appear
            const updatedConversations =
                await getCompanionConversations(id);

            setConversations(updatedConversations);


            setMessage("");

        }
        finally {

            setLoading(false);

        }

    }


    return (

        <div
            style={{
                display: "flex",
                height: "calc(100vh - 40px)",
                margin: "20px",
                fontFamily: "Segoe UI"
            }}
        >

            <CompanionChatList

                conversations={conversations}

                selectedConversationId={conversationId}

                onSelectConversation={loadConversation}

                onNewChat={startNewChat}

            />


            <div
                style={{
                    flex: 1,
                    padding: "0 25px"
                }}
            >

                <Link to="/">
                    ← Back to Companions
                </Link>


                <h1>
                    {companion?.name ?? "Loading..."}
                </h1>


                <hr />


                <div
                    style={{
                        border: "1px solid gray",
                        borderRadius: "8px",
                        padding: "15px",
                        height: "60vh",
                        overflowY: "auto",
                        marginBottom: "20px"
                    }}
                >

                    {conversation?.messages.map(message => (

                        <div
                            key={message.id}
                            style={{
                                marginBottom: "20px"
                            }}
                        >

                            <strong>
                                {message.role === "user"
                                    ? "You"
                                    : companion?.name}
                            </strong>


                            <p>
                                {message.content}
                            </p>

                        </div>

                    ))}


                    {!conversation && (

                        <p>
                            Start a new conversation with {companion?.name}.
                        </p>

                    )}

                </div>


                <input

                    value={message}

                    onChange={(e) =>
                        setMessage(e.target.value)
                    }

                    placeholder="Type your message..."

                    style={{
                        width: "80%",
                        padding: "10px"
                    }}

                />


                <button

                    onClick={handleSend}

                    disabled={loading}

                    style={{
                        marginLeft: "10px",
                        padding: "10px 20px"
                    }}

                >

                    {loading
                        ? "Sending..."
                        : "Send"}

                </button>

            </div>

        </div>

    );

}