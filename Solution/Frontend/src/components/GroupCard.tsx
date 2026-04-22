import { motion } from "framer-motion";
import { Group } from "../types/worldcup";

type GroupCardProps = {
  group: Group;
};

export function GroupCard({ group }: GroupCardProps) {
  return (
    <motion.article
      className="panel group-card-ui"
      initial={{ opacity: 0, y: 12 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ duration: 0.25 }}
      whileHover={{ y: -2 }}
    >
      <div className="group-card-ui__header">
        <div>
          <p className="group-card-ui__eyebrow">Fase de grupos</p>
          <h3>Grupo {group.label}</h3>
        </div>
        <span className="badge">
          Classificados: {group.teams[0]?.name} e {group.teams[1]?.name}
        </span>
      </div>

      <table className="standings-table">
        <thead>
          <tr>
            <th>Pos.</th>
            <th>Seleção</th>
            <th>PTS</th>
            <th>SG</th>
            <th>GP</th>
            <th>J</th>
          </tr>
        </thead>
        <tbody>
          {group.teams.map((team, index) => (
            <tr key={team.id}>
              <td className={index < 2 ? "qualified" : ""}>{index + 1}</td>
              <td>{team.name}</td>
              <td>{team.stats.points}</td>
              <td>{team.stats.goalDifference}</td>
              <td>{team.stats.goalsFor}</td>
              <td>{team.stats.played}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </motion.article>
  );
}
