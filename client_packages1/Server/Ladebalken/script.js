function Ladebalken(Typ, Zeit) {
  var bar = new ProgressBar.Line(container, {
  strokeWidth: 4,
  easing: 'easeInOut',
  duration: Zeit,
  color: '#d63b37',
  trailColor: '#ffffff',
  trailWidth: 1,
  svgStyle: {width: '100%', height: '100%'},
  text: {
    style: {
      // Text color.
      // Default: same as stroke color (options.color)
      color: '#ffffff',
      position: 'absolute',
      right: '0',
      top: '20px',
      padding: 0,
      margin: 0,
	  font: "italic bold 20px arial,serif",
      transform: null
    },
    autoStyleContainer: false
  },
  from: {color: '#FFEA82'},
  to: {color: '#ED6A5A'},
  step: (state, bar) => {
	let Prozent = Math.round(bar.value() * 100);
	if(Prozent < 20)
	{
		if(Typ == 1) { bar.setText('Wir laden deinen Account. Es dauert noch kurz ' + Prozent + ' %...'); }
		else if(Typ == 2) { bar.setText('Der Tod ist keine Alternative. Es dauert noch kurz ' + Prozent + ' %...'); }
		else if(Typ == 3) { bar.setText('Fahrzeug wird be/entladen. Es dauert noch kurz ' + Prozent + ' %...'); }
		else if(Typ == 4) { bar.setText('Leute steigen ein/aus. Es dauert noch kurz ' + Prozent + ' %...'); }
	}
	else if(Prozent > 20 && Prozent < 50)
	{
		if(Typ == 1) { bar.setText('Wir laden deinen Account. Wir haben es bald ' + Prozent + ' %...'); }
		else if(Typ == 2) { bar.setText('Der Tod ist keine Alternative. Wir haben es bald ' + Prozent + ' %...'); }
		else if(Typ == 3) { bar.setText('Fahrzeug wird be/entladen. Wir haben es bald ' + Prozent + ' %...'); }
		else if(Typ == 4) { bar.setText('Leute steigen ein/aus. Wir haben es bald ' + Prozent + ' %...'); }
	}
	else if(Prozent > 50 && Prozent < 80)
	{
		if(Typ == 1) { bar.setText('Wir laden deinen Account. Die Hälfte ist geschafft ' + Prozent + ' %...'); }
		else if(Typ == 2) { bar.setText('Der Tod ist keine Alternative. Die Hälfte ist geschafft ' + Prozent + ' %...'); }
		else if(Typ == 3) { bar.setText('Fahrzeug wird be/entladen. Die Hälfte ist geschafft ' + Prozent + ' %...'); }
		else if(Typ == 4) { bar.setText('Leute steigen ein/aus. Die Hälfte ist geschafft ' + Prozent + ' %...'); }
	}
	else if(Prozent > 80)
	{
		if(Typ == 1) { bar.setText('Wir laden deinen Account. Endspurt ' + Prozent + ' %...'); }
		else if(Typ == 2) { bar.setText('Der Tod ist keine Alternative. Endspurt ' + Prozent + ' %...'); }
		else if(Typ == 3) { bar.setText('Fahrzeug wird be/entladen. Endspurt ' + Prozent + ' %...'); }
		else if(Typ == 4) { bar.setText('Leute steigen ein/aus. Endspurt ' + Prozent + ' %...'); }
	}
    
  }
});

bar.animate(1.0); 
}