import { BrowserRouter, Routes, Route } from "react-router-dom";

import CompanionListPage from "./pages/CompanionListPage";
import CreateCompanionPage from "./pages/CreateCompanionPage";
import EditCompanionPage from "./pages/EditCompanionPage";
import ChatPage from "./pages/ChatPage";


export default function App() {

    return (
        <BrowserRouter>

            <Routes>

                <Route
                    path="/"
                    element={<CompanionListPage />}
                />


                <Route
                    path="/companions/new"
                    element={<CreateCompanionPage />}
                />


                <Route
                    path="/companions/:id/edit"
                    element={<EditCompanionPage />}
                />


                <Route
                    path="/chat/:id"
                    element={<ChatPage />}
                />

            </Routes>

        </BrowserRouter>
    );
}