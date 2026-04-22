export type TeamStats = {
  played: number;
  wins: number;
  draws: number;
  losses: number;
  goalsFor: number;
  goalsAgainst: number;
  goalDifference: number;
  points: number;
  drawSeed: number;
};

export type Team = {
  id: string;
  name: string;
  stats: TeamStats;
};

export type Group = {
  label: string;
  teams: Team[];
};

export type Match = {
  stage: string;
  extraLabel: string;
  homeGoals: number;
  awayGoals: number;
  homePenaltyGoals?: number | null;
  awayPenaltyGoals?: number | null;
  winnerId: string;
  note: string;
  homeTeam: Team;
  awayTeam: Team;
};

export type GroupStageResult = {
  groupLabel: string;
  matches: Match[];
};

export type KnockoutResult = {
  roundOf16: Match[];
  quarterFinals: Match[];
  semiFinals: Match[];
  final: Match;
};

export type FinalResultPayload = {
  equipeA: string;
  equipeB: string;
  teamAGoals: number;
  teamBGoals: number;
  teamAPenaltyGoals: number;
  teamBPenaltyGoals: number;
};

export type SimulationResponse = {
  teams: Team[];
  groups: Group[];
  groupStage: GroupStageResult[];
  knockout: KnockoutResult;
  finalResult: FinalResultPayload;
  finalResultSent: boolean;
};
