using Model;

namespace Operation
{
    public static class Bank
    {
        public static async Task<Transaction> Handler(Event data) => data switch
        {
            { Type: EventType.Deposit } => await Deposit(data),
            { Type: EventType.Transfer } => await Deposit(data),
            { Type: EventType.Withdraw } => await Withdraw(data),
            _ => throw new Exception(),
        };

        public static async Task<Transaction> Deposit(Event data)
        {
            string? id = data.Destination;

            if (id is null)
            {
                throw new Exception();
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

            decimal amount = data.Amount;
            Account updatedAccount = await Db.Handler.Increment(account, amount);
            return new() { Destination = updatedAccount };
        }

        public static async Task<Transaction> Withdraw(Event data)
        {
            string? id = data.Origin;

            if (id is null)
            {
                throw new Exception();
            }

            Account? account = await Db.Handler.Read(id);

            if (account is null)
            {
                throw new Exception();
            }

            decimal amount = data.Amount;
            Account updatedAccount = await Db.Handler.Decrement(account, amount);
            return new() { Origin = updatedAccount };
        }
    }
}