import type { Companion } from "../models/Companion";
import CompanionCard from "./CompanionCard";


interface Props {
    companions: Companion[];
}


export default function CompanionList({
    companions
}: Props) {

    return (
        <div>

            {companions.map(companion => (

                <CompanionCard
                    key={companion.id}
                    companion={companion}
                />

            ))}

        </div>
    );
}