using BroadageSportsCase.Entity;
using BroadageSportsCase.Model;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;

namespace BroadageSportsCase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Timer kullanarak 5 saniyede bir istek attım.
            TimerCallback callback = new TimerCallback(MatchListAll);
            Timer stateTimer = new Timer(callback, null, 0, 5000);

            // loop here forever //Program sonlanmasın diye sonsuz döngü
            for (; ; ) { }



        }
        static async void MatchListAll(Object stateInfo)
        {
            //Request için gerekli olan keyi app configde tanımladım ve kullanmak için burada çağırdım.
            string requestKey = ConfigurationManager.AppSettings["Ocp-Apim-Subscription-Key"];
            var client = new RestClient("https://s0-sports-data-api.broadage.com/");

            //Belirli periyotlarla istek atıcağımız için o günün tarihi ile istek attım.
            string requestDate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            var request = new RestRequest("soccer/match/list?date="+ requestDate, Method.Get);
            request.AddHeader("languageId", "2");
            request.AddHeader("Ocp-Apim-Subscription-Key", requestKey);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                /*Loglama işlemi için Serilog package kullandım.Debug klasörünü günlük bir textfile oluşturup bunun içine gün saat bazında ekliyor.
                Performans açısından daha iyi olacağını düşündüğüm için loglama işlemi text dosyasında yaptım.
                */
                var log = new LoggerConfiguration()
    .WriteTo.File("log.txt",shared:true, rollingInterval: RollingInterval.Day)
    .CreateLogger();
                log.Information(response.ErrorException.Message);
            }
            else
            {
                var myDeserializedClass = JsonConvert.DeserializeObject<BroadageSportsMatchListModel>(response.Content);

                if (myDeserializedClass.matches.Count > 0)
                {
                    //Burada transaction kullandım.Eğer ekleme işlemlerinde bir hata alırsa en başa alıcak.
                    using (BroadageSportsEntities broadageSportsEntities = new BroadageSportsEntities())
                    {
                        using (var transaction = broadageSportsEntities.Database.BeginTransaction())
                        {
                            try
                            {
                                foreach (var item in myDeserializedClass.matches)
                                {
                                    
                                    var match = broadageSportsEntities.Match.FirstOrDefault(x => x.MatchId == item.id);
                                    //Eğer maç daha önce eklenmemişse gerekli eklemeler  yapılıyor.
                                    if (match == null)
                                    {
                                        var tournamentModel = broadageSportsEntities.Tournament.FirstOrDefault(x => x.TournamentId == item.tournament.id);
                                        if (tournamentModel == null)
                                        {
                                            tournamentModel = new Entity.Tournament();
                                            tournamentModel = broadageSportsEntities.Tournament.Add(new Entity.Tournament()
                                            {
                                                TournamentId = item.tournament.id,
                                                Name = item.tournament.name,
                                                ShortName = item.tournament.shortName
                                            });
                                            broadageSportsEntities.SaveChanges();

                                        }
                                        var statusModel = broadageSportsEntities.Status.FirstOrDefault(x => x.StatusId == item.status.id);
                                        if (statusModel == null)
                                        {
                                            statusModel = new Entity.Status();
                                            statusModel = broadageSportsEntities.Status.Add(new Entity.Status()
                                            {
                                                StatusId = item.status.id,
                                                Name = item.status.name,
                                                ShortName = item.status.shortName
                                            });
                                            broadageSportsEntities.SaveChanges();

                                        }
                                        var roundModel = broadageSportsEntities.Round.FirstOrDefault(x => x.RoundId == item.round.id);
                                        if (roundModel == null)
                                        {
                                            roundModel = new Entity.Round();
                                            roundModel = broadageSportsEntities.Round.Add(new Entity.Round()
                                            {
                                                RoundId = item.round.id,
                                                Name = item.round.name,
                                                ShortName = item.round.shortName
                                            });
                                            broadageSportsEntities.SaveChanges();

                                        }
                                        var stageModel = broadageSportsEntities.Stage.FirstOrDefault(x => x.StageId == item.stage.id);
                                        if (stageModel == null)
                                        {
                                            stageModel = new Entity.Stage();
                                            stageModel = broadageSportsEntities.Stage.Add(new Entity.Stage()
                                            {
                                                StageId = item.stage.id,
                                                Name = item.stage.name,
                                                ShortName = item.stage.shortName
                                            });
                                            broadageSportsEntities.SaveChanges();

                                        }
                                        var scoreHomeTeamModel = broadageSportsEntities.Score.Add(new Entity.Score()
                                        {
                                            Current = item.homeTeam.score.current,
                                            Regular = item.homeTeam.score.regular,
                                            HalfTime = item.homeTeam.score.halfTime,


                                        });
                                        broadageSportsEntities.SaveChanges();

                                        var scoreAwayTeamModel = broadageSportsEntities.Score.Add(new Entity.Score()
                                        {
                                            Current = item.awayTeam.score.current,
                                            Regular = item.awayTeam.score.regular,
                                            HalfTime = item.awayTeam.score.halfTime,
                                        });
                                        broadageSportsEntities.SaveChanges();

                                        var homeTeamModel = broadageSportsEntities.HomeTeam.Add(new Entity.HomeTeam()
                                        {
                                            MediumName = item.homeTeam.mediumName,
                                            Name = item.homeTeam.name,
                                            ShortName = item.homeTeam.shortName,
                                            ScoreId = scoreHomeTeamModel.ScoreId
                                        });
                                        broadageSportsEntities.SaveChanges();

                                        var awayTeamModel = broadageSportsEntities.AwayTeam.Add(new Entity.AwayTeam()
                                        {
                                            MediumName = item.awayTeam.mediumName,
                                            Name = item.awayTeam.name,
                                            ShortName = item.awayTeam.shortName,
                                            ScoreId = scoreAwayTeamModel.ScoreId
                                        });
                                        broadageSportsEntities.SaveChanges();

                                        var matchModel = broadageSportsEntities.Match.Add(new Entity.Match()
                                        {
                                            AwayTeamId = awayTeamModel.AwayTeamId,
                                            HomeTeamId = homeTeamModel.HomeTeamId,
                                            MatchId = item.id,
                                            RoundId = roundModel.RoundId,
                                            StageId = stageModel.StageId,
                                            StatusId = statusModel.StatusId,
                                            TournamentId = tournamentModel.TournamentId,
                                            MatchDate = Convert.ToDateTime(item.date)
                                        });
                                        broadageSportsEntities.SaveChanges();

                                    }
                                    //Eğer maç daha önce eklenmişse score ve statü güncellemeleri yapılıyor.
                                    else
                                    {

                                        match.StatusId = item.status.id;
                                        broadageSportsEntities.SaveChanges();
                                        var scoreHomeTeamModel = broadageSportsEntities.Score.FirstOrDefault(x => x.ScoreId == match.HomeTeam.Score.ScoreId);
                                        scoreHomeTeamModel.Current = item.homeTeam.score.current;
                                        scoreHomeTeamModel.HalfTime = item.homeTeam.score.halfTime;
                                        scoreHomeTeamModel.Regular = item.homeTeam.score.regular;
                                        var scoreAwayTeamModel = broadageSportsEntities.Score.FirstOrDefault(x => x.ScoreId == match.AwayTeam.Score.ScoreId);
                                        scoreAwayTeamModel.Current = item.awayTeam.score.current;
                                        scoreAwayTeamModel.HalfTime = item.awayTeam.score.halfTime;
                                        scoreAwayTeamModel.Regular = item.awayTeam.score.regular;
                                        broadageSportsEntities.SaveChanges();
                                    }


                                }
                            }
                            catch (Exception)
                            {
                                transaction.Rollback();
                                throw;
                            }
                            transaction.Commit();

                        }

                    }
                }
            }




        }

    }
}
