using MongoDB.Bson;

namespace API.DTOs;

public class ProductDTO
{
    public ObjectId Id { get; set; }            //Table: Products | Field: id
    public string Name { get; set; }            //Table: Products | Field: name
    public int Price { get; set; }              //Table: Products | Field: price
    public string Description { get; set; }     //Table: Products | Field: description
    public string Image { get; set; }           //Table: Products | Field: image
    public string Category { get; set; }        //Table: Categoty | Field: name
}
