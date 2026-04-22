import axios from "axios";
import { SimulationResponse, Team } from "../types/worldcup";

const backendApi = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL ?? "/api",
});

const withGitUser = (gitUser: string) => ({
  headers: { "git-user": gitUser },
});

export async function fetchTeams(gitUser: string) {
  const response = await backendApi.get<Team[]>("/worldcup/teams", withGitUser(gitUser));
  return response.data;
}

export async function simulateCup(gitUser: string, sendFinalResult = true) {
  const response = await backendApi.post<SimulationResponse>(
    "/worldcup/simulate",
    { sendFinalResult },
    withGitUser(gitUser)
  );
  return response.data;
}
