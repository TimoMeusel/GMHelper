﻿using System.Collections.Generic;
using System.Linq;
using GM.Model;

namespace GM.ViewModel
{
    public class TeamRegistry
    {
        public List<TeamViewModel> Teams { get; private set; }
        public List<Skater> AllSkaters { get; private set; }
        public List<Goalie> AllGoalies { get; private set; }

        public List<TeamViewModel> CreateTeams(IEnumerable<Player> players)
        {
            Dictionary<Player, Team> teamMap = new Dictionary<Player, Team>();
            foreach ( Player player in players )
            {
                teamMap.Add(player, player.Team);
            }

            List<Team> teams = teamMap.Select(p => p.Value).Distinct().ToList();
            List<TeamViewModel> teamsViewModels = new List<TeamViewModel>();

            foreach ( var team in teams )
            {
                List<Skater> skaters =
                    teamMap.Where(t => t.Value.Equals(team)).Where(t => t.Key is Skater).Select(t => t.Key).Cast<Skater>().ToList();
                List<Goalie> goalies =
                    teamMap.Where(t => t.Value.Equals(team)).Where(t => t.Key is Goalie).Select(t => t.Key).Cast<Goalie>().ToList();

                teamsViewModels.Add(new TeamViewModel(team, skaters, goalies));
            }

            AllSkaters = teamMap.Keys.Where(p => p is Skater).Cast<Skater>().ToList();
            AllGoalies = teamMap.Keys.Where(p => p is Goalie).Cast<Goalie>().ToList();

            Teams = teamsViewModels;
            return teamsViewModels;
        }
    }
}
