using Model;

namespace Operation
{
    public static class Bank
    {
        public static async Task<Transaction?> Handler(Event data) => data switch
        {
            { Type: EventType.Deposit } => await Deposit(data),
            { Type: EventType.Transfer } => await Deposit(data),
            { Type: EventType.Withdraw } => await Deposit(data),
            _ => await Deposit(data),
        };

        public static async Task<Transaction?> Deposit(Event data)
        {
            string? id = data.Destination;

            if (id is null)
            {
                return null;
            }

            Account? account = await Db.Handler.Read(id);

            if (account is null)
            {
                Account newAccount = new()
                {
                    Id = id,
                    Balance = data.Amount
                };

                await Db.Handler.Create(newAccount);

                return new() { Destination = newAccount };
            }

            Account updatedAccount = await Db.Handler.Update(account);
            return new() { Destination = updatedAccount };
        }
    }
}