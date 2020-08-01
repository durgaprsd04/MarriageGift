
function inviteUser()
{
  document.getElementById("createEvent").setAttribute("style", "display:none");
  document.getElementById("InviteUser").setAttribute("style", "display:block");
  document.getElementById("modifyEvent").setAttribute("style", "display:none");
  document.getElementById("BuyGiftForInvite").setAttribute("style", "display:none");
  document.getElementById("RemoveGiftForInvite").setAttribute("style", "display:none");
  document.getElementById("RespondToInvite").setAttribute("style", "display:none");
    document.getElementById("ChangePassword").setAttribute("style", "display:none");
}
function modifyEvent()
{
  document.getElementById("createEvent").setAttribute("style", "display:none");
  document.getElementById("InviteUser").setAttribute("style", "display:none");
  document.getElementById("modifyEvent").setAttribute("style", "display:block");
  document.getElementById("BuyGiftForInvite").setAttribute("style", "display:none");
  document.getElementById("RemoveGiftForInvite").setAttribute("style", "display:none");
  document.getElementById("RespondToInvite").setAttribute("style", "display:none");
    document.getElementById("ChangePassword").setAttribute("style", "display:none");
}
function BuyGiftForInvite()
{
  document.getElementById("createEvent").setAttribute("style", "display:none");
  document.getElementById("InviteUser").setAttribute("style", "display:none");
  document.getElementById("modifyEvent").setAttribute("style", "display:none");
  document.getElementById("BuyGiftForInvite").setAttribute("style", "display:block");
  document.getElementById("RemoveGiftForInvite").setAttribute("style", "display:none");
  document.getElementById("RespondToInvite").setAttribute("style", "display:none");
    document.getElementById("ChangePassword").setAttribute("style", "display:none");
}

function RemoveGiftForInvite()
{
  document.getElementById("createEvent").setAttribute("style", "display:none");
  document.getElementById("InviteUser").setAttribute("style", "display:none");
  document.getElementById("modifyEvent").setAttribute("style", "display:none");
  document.getElementById("BuyGiftForInvite").setAttribute("style", "display:none");
  document.getElementById("RemoveGiftForInvite").setAttribute("style", "display:block");
  document.getElementById("RespondToInvite").setAttribute("style", "display:none");
    document.getElementById("ChangePassword").setAttribute("style", "display:none");
}

function RespondToInvite()
{
  document.getElementById("createEvent").setAttribute("style", "display:none");
  document.getElementById("InviteUser").setAttribute("style", "display:none");
  document.getElementById("modifyEvent").setAttribute("style", "display:none");
  document.getElementById("BuyGiftForInvite").setAttribute("style", "display:none");
  document.getElementById("RemoveGiftForInvite").setAttribute("style", "display:none");
  document.getElementById("RespondToInvite").setAttribute("style", "display:block");
  document.getElementById("ChangePassword").setAttribute("style", "display:none");
}
function createEvent1()
{
  document.getElementById("createEvent").setAttribute("style", "display:inline");
  document.getElementById("InviteUser").setAttribute("style", "display:none");
  document.getElementById("modifyEvent").setAttribute("style", "display:none");
  document.getElementById("BuyGiftForInvite").setAttribute("style", "display:none");
  document.getElementById("RemoveGiftForInvite").setAttribute("style", "display:none");
  document.getElementById("RespondToInvite").setAttribute("style", "display:none");
  document.getElementById("ChangePassword").setAttribute("style", "display:none");
}
function CreateEventDone() {
document.getElementById("createEvent").setAttribute("style", "display:none");
    alert("donestuff");
}
function ValidateEventForm() {
    var isValid=true;
    var occassion =document.getElementById("occasion")
    var selectedOccasion = occassion.options[occassion.selectedIndex].value;
    var eventPlace = document.getElementById("eventPlace").value;
    var eventDate = document.getElementById("eventDate").value;
    var eventTime = document.getElementById("eventTime").value;
    var giftList =  $('#giftList').val();
    var event1={};
    event1["occassion"]=selectedOccasion;
    event1["place"]=eventPlace;
    event1["date"]=eventDate;
    event1["time"]=eventTime;
    event1["expectedGiftList"]=giftList;
    return isValid;
}
function ValidateInviteForm()
{
  
}
function ChangePassword()
{
  document.getElementById("createEvent").setAttribute("style", "display:none");
  document.getElementById("InviteUser").setAttribute("style", "display:none");
  document.getElementById("modifyEvent").setAttribute("style", "display:none");
  document.getElementById("BuyGiftForInvite").setAttribute("style", "display:none");
  document.getElementById("RemoveGiftForInvite").setAttribute("style", "display:none");
  document.getElementById("RespondToInvite").setAttribute("style", "display:none");
  document.getElementById("ChangePassword").setAttribute("style", "display:block");
}
