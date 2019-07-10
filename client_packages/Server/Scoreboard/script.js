let scrollHeight;

setInterval(ScrollHeight, 1000);

function ScrollHeight() {
  var objekt = document.getElementById("scoreBoard");
	scrollHeight = objekt.scrollHeight;
	mp.trigger('ScoreboardScrollHeight', scrollHeight);
}
