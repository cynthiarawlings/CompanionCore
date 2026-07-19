import { useNavigate } from "react-router-dom";

import CompanionForm from "../components/CompanionForm";
import { createCompanion } from "../api/companionApi";


export default function CreateCompanionPage() {

    const navigate = useNavigate();


    async function handleCreate(
        companion: Parameters<typeof createCompanion>[0]
    ) {

        await createCompanion(companion);

        navigate("/");
    }


    return (
        <div
            style={{
                maxWidth: "900px",
                margin: "40px auto",
                fontFamily: "Segoe UI"
            }}
        >

            <h1>Create Companion</h1>


            <CompanionForm
                onSubmit={handleCreate}
            />

        </div>
    );
}