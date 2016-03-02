<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="LFCRM.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.10.2.js"></script>
    <script type="text/javascript">
        var el = $('.floating');
        var elpos_original = el.offset().top;

        $(window).scroll(function () {
            var elpos = el.offset().top;
            var windowpos = $(window).scrollTop();
            var finaldestination = windowpos;
            if (windowpos < elpos_original) {
                finaldestination = elpos_original;
                el.stop().animate({ 'top': 400 }, 500);
            } else {
                el.stop().animate({ 'top': windowpos + 10 }, 500);
            }
        });
    </script>

</head>
<body>
    <form id="form1" runat="server" style="font-family: Tahoma; font-size: 10pt">
        <div class="box follow-scroll" id="floating">
            Third box (follows screen)
        </div>
        <div class="wrapper group">
    <div class="content">
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam euismod arcu ut diam maximus auctor. Sed malesuada maximus tellus, dignissim volutpat urna hendrerit at. Cras nisi turpis, mattis vitae ornare ut, bibendum et massa. Nulla at tellus a arcu luctus ultrices ac non felis. Maecenas euismod efficitur ipsum vel vestibulum. Nam imperdiet ipsum nunc, sit amet varius felis maximus quis. Etiam efficitur, mauris placerat luctus facilisis, ligula sem pellentesque nunc, ac viverra tellus velit sit amet purus. Proin a nunc id quam tempor blandit vitae id orci. Cras lectus turpis, varius nec elementum venenatis, suscipit ut nisl.</p>
        <p>Nulla condimentum est in leo mollis, sit amet varius erat pulvinar. Donec posuere turpis non est ultrices lobortis. Donec commodo aliquam sodales. Ut id finibus velit. Aenean tempus eget nulla at condimentum. Ut a arcu quis dui ultricies efficitur. Vestibulum suscipit diam ullamcorper velit tempor, in ullamcorper odio sollicitudin. Vestibulum congue nisl nibh, ut elementum quam tristique non. Nulla in sollicitudin dolor. Morbi ac justo nulla. Suspendisse massa neque, vestibulum id dolor non, congue rhoncus nibh.</p>
        <p>Phasellus porta tellus vel ipsum vehicula, nec fermentum lectus porta. Aenean viverra magna eu risus lacinia malesuada. Sed molestie auctor pharetra. Aliquam erat volutpat. Vestibulum in mi a orci tincidunt pellentesque. Sed et lectus eros. Nam imperdiet neque eu dolor gravida, interdum pellentesque justo rhoncus. Morbi tincidunt, ex nec pellentesque pulvinar, dui nulla tempor ipsum, sed consequat est tortor at neque. Suspendisse sed consequat urna. Nullam luctus, sem convallis volutpat mollis, sapien odio finibus elit, vitae fringilla enim leo sed velit. Vivamus fringilla ante laoreet blandit porta. Sed condimentum ut erat nec dignissim.</p>
        <p>Morbi ac scelerisque metus. Donec rhoncus diam urna, nec aliquet ex mollis ut. Sed a arcu ac risus semper pellentesque ut non nibh. Phasellus eu ullamcorper sem. Maecenas at tortor faucibus, consequat risus sed, egestas sapien. Suspendisse tortor lacus, congue sed velit vel, dictum sagittis eros. Proin eu nisl viverra, mattis velit vitae, tempor turpis. Ut sodales lacus in iaculis faucibus. Integer non metus non nulla malesuada rutrum ac non ipsum. Vivamus quam diam, suscipit sed velit vel, tincidunt imperdiet urna. Praesent dapibus augue a dignissim lacinia. Nullam pharetra volutpat ligula, quis aliquet mauris pharetra nec. Etiam finibus, neque in laoreet vehicula, lorem justo feugiat velit, ac accumsan neque lacus non tellus. Aliquam quis sagittis massa, a bibendum enim.</p>
        <p>Morbi vel elementum libero, vitae viverra est. Pellentesque sollicitudin neque at ligula suscipit, vel faucibus mauris consequat. Donec quis pharetra nulla, at tristique lacus. Nunc vel magna ultricies, hendrerit purus et, rhoncus dolor. Aliquam erat volutpat. Phasellus auctor malesuada augue, a iaculis sem mattis quis. Nulla facilisi.</p>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
    <div class="sidebar">
        <div class="box">
            First box
        </div>
        <div class="box">
            Second box
        </div>
        
    </div>
</div>

    </form>
</body>
</html>
