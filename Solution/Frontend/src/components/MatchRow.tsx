import { Match } from "../types/worldcup";

type MatchRowProps = {
  match: Match;
};

export function MatchRow({ match }: MatchRowProps) {
  const penalties =
    match.homePenaltyGoals !== undefined && match.homePenaltyGoals !== null
      ? ` | Pen: ${match.homePenaltyGoals} x ${match.awayPenaltyGoals}`
      : "";

  return (
    <article className="match-row">
      <div className="match-row__meta">
        <strong>{match.stage}</strong>
        <span>{match.extraLabel}</span>
      </div>
      <div className="match-row__score">
        <span>{match.homeTeam.name}</span>
        <strong>{match.homeGoals}</strong>
      </div>
      <div className="match-row__score">
        <span>{match.awayTeam.name}</span>
        <strong>{match.awayGoals}</strong>
      </div>
      <small>{match.note}{penalties}</small>
    </article>
  );
}
