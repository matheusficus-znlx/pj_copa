import { motion } from "framer-motion";
import { FinalResultPayload, Match } from "../types/worldcup";

type FinalResultProps = {
  result: FinalResultPayload | null;
  finalMatch: Match | null;
};

export function FinalResult({ result, finalMatch }: FinalResultProps) {
  if (!result || !finalMatch) {
    return <div className="panel">Nenhuma final simulada ainda.</div>;
  }

  const penalties =
    result.teamAPenaltyGoals || result.teamBPenaltyGoals
      ? ` | Pen: ${result.teamAPenaltyGoals} x ${result.teamBPenaltyGoals}`
      : "";
  const champion =
    finalMatch.winnerId === finalMatch.homeTeam.id
      ? finalMatch.homeTeam.name
      : finalMatch.awayTeam.name;

  return (
    <motion.section
      className="panel final-result-card"
      initial={{ opacity: 0, y: 12 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ duration: 0.3 }}
    >
      <div className="final-result-card__header">
        <div>
          <p className="group-card-ui__eyebrow">Grande decisão</p>
          <h3>Final</h3>
        </div>
        <span className="badge badge--highlight">Resultado oficial</span>
      </div>
      <p className="final-result-card__score">
        {finalMatch.homeTeam.name} {finalMatch.homeGoals} x {finalMatch.awayGoals} {finalMatch.awayTeam.name}
        {penalties}
      </p>
      <div className="final-result-card__winner">
        <span className="group-card-ui__eyebrow">Taça da simulação</span>
        <strong>{champion}</strong>
      </div>
      <div className="final-result-card__meta">
        <div className="status-box">
          <strong>Decisão</strong>
          <span>{finalMatch.note}</span>
        </div>
        <div className="status-box">
          <strong>Payload enviado</strong>
          <span>{result.equipeA} vs {result.equipeB}</span>
        </div>
      </div>
    </motion.section>
  );
}
