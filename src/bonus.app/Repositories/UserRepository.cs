using System;
using System.Collections.Generic;
using AutoMapper;
using battery.app.Core.RealmObjects;
using bonus.app.Core.Models;
using bonus.app.Core.RealmObjects;
using Realms;

namespace bonus.app.Core.Repositories
{
	public class UserRepository : IUserRepository
	{
		#region Data
		#region Fields
		private readonly IMapper _mapper;
		#endregion
		#endregion

		#region .ctor
		public UserRepository()
		{
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<AccessToken, AccessTokenRealmObject>();
				cfg.CreateMap<AccessTokenRealmObject, AccessToken>();

				cfg.CreateMap<User, UserRealmObject>()
				   .ForMember(m => m.Birthday, o => o.MapFrom(q => q.Birthday == DateTime.MinValue ? DateTimeOffset.MinValue : q.Birthday))
				   .ForMember(m => m.Guid, o => o.MapFrom(q => q.Uuid.ToString()))
				   .ForPath(m => m.AccessToken.Body, o => o.MapFrom(q => q.AccessToken.Body))
				   .ForPath(m => m.AccessToken.Type, o => o.MapFrom(q => q.AccessToken.Type));
				cfg.CreateMap<UserRealmObject, User>()
				   .ForMember(m => m.Birthday, o => o.MapFrom(q => q.Birthday.Date))
				   .ForMember(m => m.Uuid, o => o.MapFrom(q => Guid.Parse(q.Guid)))
				   .ForPath(m => m.AccessToken.Body, o => o.MapFrom(q => q.AccessToken.Body))
				   .ForPath(m => m.AccessToken.Type, o => o.MapFrom(q => q.AccessToken.Type));

			}));
		}
		#endregion

		#region Public
		public void Add(User user)
		{
			using (var realm = Realm.GetInstance())
			{
				var userRealm = _mapper.Map<UserRealmObject>(user);
				using (var transaction = realm.BeginWrite())
				{
					realm.Add(userRealm, true);
					transaction.Commit();
				}
			}
		}

		public IEnumerable<User> GetAll()
		{
			using (var realm = Realm.GetInstance())
			{
				var users = realm.All<UserRealmObject>();
				var userList = new List<User>();
				foreach (var user in users)
				{
					userList.Add(_mapper.Map<User>(user));
				}

				return userList;
			}
		}

		public bool Remove(User user)
		{
			using (var realm = Realm.GetInstance())
			{
				using (var transaction = realm.BeginWrite())
				{
					try
					{
						var userRealm = realm.Find<UserRealmObject>(user?.Uuid.ToString());

						realm.Remove(userRealm);

					}
					catch (Exception e)
					{
						Console.WriteLine(e);
						return false;
					}
					transaction.Commit();
				}
			}

			return true;
		}

		public void RemoveAll()
		{
			using (var realm = Realm.GetInstance())
			{
				using (var transaction = realm.BeginWrite())
				{
					realm.RemoveAll<UserRealmObject>();
					transaction.Commit();
				}
			}
		}

		public User Find(Guid uuid)
		{
			using (var realm = Realm.GetInstance())
			{
				var user = realm.Find<UserRealmObject>(uuid.ToString());
				return _mapper.Map<User>(user);
			}
		}

		public bool Update(User user)
		{
			using (var realm = Realm.GetInstance())
			{
				using (var transaction = realm.BeginWrite())
				{
					try
					{
						var userRealm = realm.Find<UserRealmObject>(user.Uuid.ToString());
						realm.Remove(userRealm);
						realm.Add(_mapper.Map<UserRealmObject>(user), true);
						transaction.Commit();
					}
					catch (Exception e)
					{
						transaction.Rollback();
						Console.WriteLine(e);
						return false;
					}
				}
			}

			return true;
		}
		#endregion
	}
}
