import { useState } from "react";
import type { Companion } from "../models/Companion";


interface Props {
    initialData?: Omit<Companion, "id">;

    onSubmit: (
        companion: Omit<Companion, "id">
    ) => Promise<void>;
}

export default function CompanionForm({
    initialData,
    onSubmit
}: Props) {


const [form, setForm] = useState(
    initialData ?? {
        name: "",
        universe: "",
        description: "",
        personalitySummary: "",
        speakingStyle: "",
        systemPrompt: ""
    }
);


    function updateField(
        field: string,
        value: string
    ) {
        setForm({
            ...form,
            [field]: value
        });
    }



    async function handleSubmit(
        e: React.FormEvent
    ) {

        e.preventDefault();

        await onSubmit(form);
    }



    return (
        <form onSubmit={handleSubmit}>

            <input
                placeholder="Name"
                value={form.name}
                onChange={e =>
                    updateField(
                        "name",
                        e.target.value
                    )
                }
            />

            <br></br>


            <input
                placeholder="Universe"
                value={form.universe}
                onChange={e =>
                    updateField(
                        "universe",
                        e.target.value
                    )
                }
            />

            <br></br>

            <textarea
                placeholder="Description"
                value={form.description}
                onChange={e =>
                    updateField(
                        "description",
                        e.target.value
                    )
                }
            />

            <br></br>

            <textarea
                placeholder="Personality Summary"
                value={form.personalitySummary}
                onChange={e =>
                    updateField(
                        "personalitySummary",
                        e.target.value
                    )
                }
            />

            <br></br>

            <textarea
                placeholder="Speaking Style"
                value={form.speakingStyle}
                onChange={e =>
                    updateField(
                        "speakingStyle",
                        e.target.value
                    )
                }
            />

            <br></br>

            <textarea
                placeholder="System Prompt"
                value={form.systemPrompt}
                onChange={e =>
                    updateField(
                        "systemPrompt",
                        e.target.value
                    )
                }
            />

            <br></br>


            <button type="submit">
                Save Companion
            </button>

        </form>
    );
}