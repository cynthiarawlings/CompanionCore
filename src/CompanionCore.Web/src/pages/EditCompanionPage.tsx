import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import CompanionForm from "../components/CompanionForm";

import {
    getCompanion,
    updateCompanion,
    deleteCompanion
} from "../api/companionApi";

import type { Companion } from "../models/Companion";


export default function EditCompanionPage() {

    const { id } = useParams();

    const navigate = useNavigate();


    const [companion, setCompanion] =
        useState<Companion | null>(null);



    useEffect(() => {

        async function load() {

            if (!id) return;

            const data = await getCompanion(id);

            setCompanion(data);
        }


        load();

    }, [id]);



    async function handleUpdate(
        data: Omit<Companion, "id">
    ) {

        if (!id) return;

        await updateCompanion(
            id,
            data
        );


        navigate("/");
    }

    async function handleDelete() {

    if (!id) return;


    const confirmed = window.confirm(
        "Are you sure you want to delete this companion? This cannot be undone."
    );


    if (!confirmed) {
        return;
    }


    await deleteCompanion(id);


    navigate("/");
}



    if (!companion) {
        return <p>Loading...</p>;
    }



    return (

        <div>

            <h1>Edit Companion</h1>


            <CompanionForm
                initialData={companion}
                onSubmit={handleUpdate}
            />

           <hr />


            <div
                style={{
                    marginTop: "30px",
                    padding: "20px",
                    border: "1px solid #ff6666",
                    borderRadius: "8px"
                }}
            >

                <h2>
                    Danger Zone
                </h2>


                <p>
                    Deleting this companion is permanent.
                    All associated conversations will also be removed
                    in the future.
                </p>


                <button
                    onClick={handleDelete}
                >
                    Delete Companion
                </button>

            </div> 

        </div>
    );
}