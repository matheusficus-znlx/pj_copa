import { motion } from "framer-motion";
import { Match } from "../types/worldcup";
import { MatchRow } from "./MatchRow";

type BracketProps = {
  title: string;
  matches: Match[];
};

export function Bracket({ title, matches }: BracketProps) {
  return (
    <motion.section
      className="panel bracket-stage"
      initial={{ opacity: 0, y: 12 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ duration: 0.3 }}
      whileHover={{ y: -2 }}
    >
      <div className="bracket-stage__header">
        <p className="group-card-ui__eyebrow">Mata-mata</p>
        <h3>{title}</h3>
      </div>
      <div className="bracket-stage__matches">
        {matches.map((match) => (
          <MatchRow key={match.stage} match={match} />
        ))}
      </div>
    </motion.section>
  );
}
