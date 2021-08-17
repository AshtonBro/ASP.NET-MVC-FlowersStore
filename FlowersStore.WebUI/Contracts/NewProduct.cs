namespace FlowersStore.WebUI.Contracts
{
    public class NewProduct
    {
        public byte[] Image { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public decimal Price { get; set; }
    }
}