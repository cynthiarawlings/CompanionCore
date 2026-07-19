import { useEffect, useState } from "react";
import { Link } from "react-router-dom";

import type { Companion } from "../models/Companion";

import {
    getCompanions
} from "../api/companionApi";

import CompanionList from "../components/CompanionList";

export default function CompanionListPage() {

    const [companions, setCompanions] =
        useState<Companion[]>([]);

    async function loadCompanions() {
        const data = await getCompanions();
        setCompanions(data);
    }

    useEffect(() => {
        loadCompanions();
    }, []);

    return (
        <div
            style={{
                maxWidth: "900px",
                margin: "40px auto",
                fontFamily: "Segoe UI"
            }}
        >
            <h1>CompanionCore</h1>

            <hr />

            <h2>Companions</h2>

            <Link to="/companions/new">
                <button>
                    + New Companion
                </button>
            </Link>

            <br />
            <br />

            <CompanionList
                companions={companions}
            />
        </div>
    );
}