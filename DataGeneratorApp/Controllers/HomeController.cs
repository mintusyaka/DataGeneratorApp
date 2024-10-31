using DataGeneratorApp.Models;
using DataGeneratorApp.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace DataGeneratorApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Generator generator) {
			using (var context = new CharityGameContext())
            {
				for (int i = 0; i < generator.LevelCount; ++i)
				{
                    var level = new Level();
					if(context.Levels.Count() > 0)
	                    level.LevelId = context.Levels.Max(x => x.LevelId) + 1 + i;
					else
						level.LevelId = 1 + i;
					
					RandomGenerator.RandomLevelGenerator(level);
					context.Levels.Add(level);
				}

				for (int i = 0; i < generator.PlayerCount; ++i)
				{
					var player = new Player();
					RandomGenerator.RandomPlayerGenerator(player);
                    context.Players.Add(player);
					context.SaveChanges();

					player.PlayerId = context.Players.Max(x => x.PlayerId);

					var sql = $"INSERT INTO player_activity (player_id) VALUES ({player.PlayerId})";
					context.Database.ExecuteSqlRaw(sql);
				}

                for (int i = 0; i < generator.CharacterCount; ++i)
                {
                    var character = new Character();
					
					if (context.Characters.Count() > 0)
						character.CharacterId = context.Characters.Max(x => x.CharacterId) + 1 + i;
					else
						character.CharacterId = 1 + i;
					
					RandomGenerator.RandomCharacterGenerator(character);

                    context.Characters.Add(character);
                }

				for (int i = 0; i < generator.ItemCount; ++i)
				{
					var item = new Item();

					if (context.Items.Count() > 0)
						item.ItemId = context.Items.Max(x => x.ItemId) + 1 + i;
					else
						item.ItemId = 1 + i;

					RandomGenerator.RandomItemGenerator(item);
					context.Items.Add(item);
				}

				for(int i = 0; i < generator.CharityFundCount; ++i)
				{
					var charityFund = new CharityFund();
					RandomGenerator.RandomCharityFundGenerator(charityFund);
					context.CharityFunds.Add(charityFund);
					context.SaveChanges();
				}


				for (int i = 0; i < generator.TransactionCount; ++i)
				{
					var transaction = new Transaction();

					RandomGenerator.RandomTransactionGenerator(transaction, context.Players.ToList(), context.CharityFunds.ToList());
					
					context.Transactions.Add(transaction);
				}

				for(int i = 0; i < 5; ++i)
				{
					var pl = new PlayersLevel();
					var pi = new PlayersItem();
					var pc = new PlayersCharacter();
					var lc = new LevelsCharacter();
					var ci = new CharactersItem();
					RandomGenerator.RandomPlayersLevelsGenerator(pl, context.Players.ToList(), context.Levels.ToList());
					RandomGenerator.RandomCharactersItemsGenerator(ci, context.Characters.ToList(), context.Items.ToList());
					RandomGenerator.RandomCharactersLevelsGenerator(lc, context.Characters.ToList(), context.Levels.ToList());
					RandomGenerator.RandomPlayersCharactersGenerator(pc, context.Players.ToList(), context.Characters.ToList());
					RandomGenerator.RandomPlayersItemsGenerator(pi, context.Players.ToList(), context.Items.ToList());

					var sql = $"INSERT INTO players_items (player_id, item_id) VALUES ({pi.PlayerId}, {pi.ItemId})";
					context.Database.ExecuteSqlRaw(sql);

					sql = $"INSERT INTO players_levels (player_id, level_id) VALUES ({pl.PlayerId}, {pl.LevelId})";
					context.Database.ExecuteSqlRaw(sql);


					sql = $"INSERT INTO players_characters (player_id, character_id) VALUES ({pc.PlayerId}, {pc.CharacterId})";
					context.Database.ExecuteSqlRaw(sql);

					sql = $"INSERT INTO levels_characters (level_id, character_id) VALUES ({lc.LevelId}, {lc.CharacterId})";
					context.Database.ExecuteSqlRaw(sql);

					sql = $"INSERT INTO characters_items (character_id, item_id) VALUES ({ci.CharacterId}, {ci.ItemId})";
					context.Database.ExecuteSqlRaw(sql);
				}


				context.SaveChanges();

			}



			return View(generator);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

	public static class RandomGenerator
	{
		public static void RandomLevelGenerator(Level level)
        {
            List<string> TitleFirstPartArr = new List<string>()
            {
                "Turtle", "Qwerty", "Tiger", "Sky", "Fauna"
            };
			List<string> TitleSecondPartArr = new List<string>()
			{
				"River", "City", "Swamp", "Forest", "Island"
			};

			Random random = new Random();

            level.Title = TitleFirstPartArr[random.Next(0, 4)] + " " + TitleSecondPartArr[random.Next(0, 4)];
            level.Description = level.Title + " Description" + random.Next(1, 100).ToString();

            level.CreationDate = new DateOnly();
            level.CreationTime = new TimeOnly();

            level.UnlockScore = random.Next(0, 50);
		}

		public static void RandomPlayerGenerator(Player player)
		{
			List<string> NicknameChar = new List<string>()
			{
				"A", "a", "B", "b", "C", "c", "D", "d", "E", "e", "F", "f", "G", "g", "H", "h", "I", "i", "K", "k", "L", "l", "M", "m", "N", "n"
			};
            List<string> SexChar = new List<string>()
            {
                "M", "F"
            };

			Random random = new Random();

            int nicknameLength = random.Next(6, 16);

            player.Nickname = "";
            for(int i = 0; i < nicknameLength; i++)
            {
                player.Nickname += NicknameChar[random.Next(0, NicknameChar.Count - 1)];
            }

            player.Password = "123123";

            player.Email = player.Nickname + "@gmail.com";

            player.Sex = SexChar[random.Next(0, 1)];
		}

		public static void RandomCharacterGenerator(Character character)
		{
			List<string> NicknameChar = new List<string>()
			{
				"A", "a", "B", "b", "C", "c", "D", "d", "E", "e", "F", "f", "G", "g", "H", "h", "I", "i", "K", "k", "L", "l", "M", "m", "N", "n"
			};

			Random random = new Random();

			int nameLength = random.Next(3, 10);

			character.Name = "";
			for (int i = 0; i < nameLength; i++)
			{
				character.Name += NicknameChar[random.Next(0, NicknameChar.Count - 1)];
			}

            character.Description = character.Name + "Description";

            character.Strength = random.Next(0, 100);
            character.Speed = random.Next(0, 100);
		}

		public static void RandomItemGenerator(Item item)
		{
			List<string> NicknameChar = new List<string>()
			{
				"A", "a", "B", "b", "C", "c", "D", "d", "E", "e", "F", "f", "G", "g", "H", "h", "I", "i", "K", "k", "L", "l", "M", "m", "N", "n"
			};
			List<string> ItemType = new List<string>()
			{
				"weapon", "armor", "food"
			};
			List<string> Rarity = new List<string>()
			{
				"normal", "rare", "epic", "legendary"
			};

			Random random = new Random();

			int titleLength = random.Next(3, 16);

			item.Title= "";
			for (int i = 0; i < titleLength; i++)
			{
				item.Title += NicknameChar[random.Next(0, NicknameChar.Count - 1)];
			}

			item.Description = item.Title + " Description";
			item.Rarity = Rarity[random.Next(0, 3)];
			item.ItemType = ItemType[random.Next(ItemType.Count - 1)];
			item.Score = random.Next(100);
		}

		public static void RandomCharityFundGenerator(CharityFund charityFund)
		{
			List<string> NicknameChar = new List<string>()
			{
				"A", "a", "B", "b", "C", "c", "D", "d", "E", "e", "F", "f", "G", "g", "H", "h", "I", "i", "K", "k", "L", "l", "M", "m", "N", "n"
			};

			Random random = new Random();

			int nameLength = random.Next(3, 10);

			charityFund.Title = "";
			for (int i = 0; i < nameLength; i++)
			{
				charityFund.Title += NicknameChar[random.Next(0, NicknameChar.Count - 1)];
			}

			charityFund.Description = charityFund.Title + "Description";

			charityFund.CardNumber = random.Next(0, 9999).ToString() + random.Next(0, 9999).ToString() + random.Next(0, 9999).ToString() + random.Next(0, 9999).ToString();
			charityFund.TelephoneNumber = random.Next(99999).ToString() + random.Next(99999).ToString();
		}
		public static void RandomTransactionGenerator(Transaction transaction, List<Player> newPlayers, List<CharityFund> charityFunds)
		{
			Random random = new Random();

			transaction.PlayerId = newPlayers[random.Next(newPlayers.Count())].PlayerId;
			transaction.CharityFundId = charityFunds[random.Next(charityFunds.Count())].CharityFundId;

			transaction.MoneyCount = random.NextDouble() + random.Next(100);

			transaction.TransactionTime = new TimeOnly();
			transaction.TransactionDate = new DateOnly();
		}
		public static void RandomPlayersLevelsGenerator(PlayersLevel pl, List<Player> players, List<Level> levels)
		{
			Random random = new Random();

			pl.PlayerId = players[random.Next(players.Count() - 1)].PlayerId;
			pl.LevelId = levels[random.Next(levels.Count() - 1)].LevelId;
		}
		public static void RandomPlayersItemsGenerator(PlayersItem pl, List<Player> players, List<Item> items)
		{
			Random random = new Random();

			pl.PlayerId = players[random.Next(players.Count() - 1)].PlayerId;
			pl.ItemId = items[random.Next(items.Count() - 1)].ItemId;
		}

		public static void RandomPlayersCharactersGenerator(PlayersCharacter pl, List<Player> players, List<Character> characters)
		{
			Random random = new Random();

			pl.PlayerId = players[random.Next(players.Count() - 1)].PlayerId;
			pl.CharacterId = characters[random.Next(characters.Count() - 1)].CharacterId;
		}

		public static void RandomCharactersLevelsGenerator(LevelsCharacter pl, List<Character> characters, List<Level> levels)
		{
			Random random = new Random();

			pl.CharacterId = characters[random.Next(characters.Count() - 1)].CharacterId;
			pl.LevelId = levels[random.Next(levels.Count() - 1)].LevelId;
		}

		public static void RandomCharactersItemsGenerator(CharactersItem pl, List<Character> characters, List<Item> items)
		{
			Random random = new Random();

			pl.CharacterId = characters[random.Next(characters.Count() - 1)].CharacterId;
			pl.ItemId = items[random.Next(items.Count() - 1)].ItemId;
		}

	}
}
