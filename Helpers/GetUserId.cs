using FlowersStore.Data;
using System;
using System.Linq;

public class GetId
{
    public Guid GetIdUser(string userName)
    {
        if (string.IsNullOrEmpty(userName)) throw new ArgumentException("UserName can't be empty.");
        using (StoreDBContext db = new StoreDBContext())
        {
            return db.Users.FirstOrDefault(f => f.Name == userName).UserId;
        }
    }
}