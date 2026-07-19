import { Link } from "react-router-dom";
import type { Companion } from "../models/Companion";


interface Props {
    companion: Companion;
}


export default function CompanionCard({
    companion
}: Props) {

    return (
        <div
            style={{
                border: "1px solid #ccc",
                borderRadius: "8px",
                padding: "20px",
                marginBottom: "15px"
            }}
        >

            <h3>
                {companion.name}
            </h3>


            <p>
                <strong>Universe:</strong>{" "}
                {companion.universe}
            </p>


            <p>
                {companion.description}
            </p>


            <div
                style={{
                    marginTop: "15px"
                }}
            >

                <Link to={`/chat/${companion.id}`}>
                    <button>
                        Chat
                    </button>
                </Link>


                {" "}


                <Link to={`/companions/${companion.id}/edit`}>
                    <button>
                        Edit
                    </button>
                </Link>

            </div>

        </div>
    );
}