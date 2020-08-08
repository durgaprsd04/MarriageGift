using Newtonsoft.Json;
namespace MarriageGiftAPI.Controllers
{

  public class Customer
  {
    public  string id{get;set;}
    public string username{get;set;}
    public string password {get;set;}
    [JsonConstructor]
    public Customer(string id,string username, string password)
    {
      this.id=id;
      this.username=username;
      this.password=password;
    }
  }
}
