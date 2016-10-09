namespace WinHibernate.Domain
{

    public class Item {
        public virtual string Itemid { get; set; }
        public virtual string Productid { get; set; }
        public virtual decimal? Listprice { get; set; }
        public virtual decimal? Unitcost { get; set; }
        public virtual int? Supplier { get; set; }
        public virtual string Status { get; set; }
        public virtual string Name { get; set; }
        public virtual string Image { get; set; }
    }
}
