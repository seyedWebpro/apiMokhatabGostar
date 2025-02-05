public class serviceresult
{
    public serviceresult()
    {
        this.messageType = messageType.ok;
        message = "";
        errorProps = new List<errorProp>();
    }

    public messageType messageType { get; set; }
    public string message { get; set; }
    public object objectResult { get; set; }
    public List<errorProp> errorProps { get; set; }

}

public class errorProp
{
    public string propname { get; set; }
    public string error { get; set; }

    //init
    public void init(ref serviceresult result)
    {
        result.errorProps.Add(this);
        result.messageType = messageType.error; 
        result.message = result.errorProps[0].error;
    }
}


public enum messageType{
    ok = 0,
    error = 1,
}