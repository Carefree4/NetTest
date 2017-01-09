namespace NetTest.Services.IpInformation
{
    public interface IIpInformation
    {
        IpLocation GetIpLocation(string address);
    }
}