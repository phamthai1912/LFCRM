<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        Application["SoLuotTruyCap"] = 1143;
        Application["tructuyen"] = 107;

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
        Application["SoLuotTruyCap"] = (int)Application["SoLuotTruyCap"] + 1;
        Application["tructuyen"] = (int)Application["tructuyen"] + 1;
        Session["DaDangNhap"] = false;
        Session["ShoppingCart"] = new csShoppingCart();
    }

    void Session_End(object sender, EventArgs e) 
    {
        Application["tructuyen"] = (int)Application["tructuyen"] - 1;
    }
       
</script>
