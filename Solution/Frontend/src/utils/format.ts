export function formatScore(homeGoals: number, awayGoals: number) {
  return `${homeGoals} x ${awayGoals}`;
}

export function formatStage(stageName: string) {
  return stageName.toUpperCase();
}
