
//init: start up MIDI
window.addEventListener('load', function() {
                        if (navigator.requestMIDIAccess)
                        navigator.requestMIDIAccess().then( onMIDIStarted, onMIDISystemError );
                        
                        });


var midiAccess = null;
var midiIn = null;
var midiOut = null;
var bassStationFound = false;

function onMIDISystemError( msg )
{
    console.log( "Failed to get MIDI access - " + msg );
    promptJazzPlugin();
}

function onMIDIStarted( midi )
{
    midiAccess = midi;
    
    // inputs
    var inputs= midiAccess.inputs.values();
    
    for ( var input = inputs.next(); input && !input.done; input = inputs.next()){
        input = input.value;
        
        if (input.name.toString().indexOf("MIDI Port") != -1) {
            
            midiIn = input;
            midiIn.onmidimessage = midiProc;
            bassStationFound = true;
        }
    }
    
    // output
    var outputs= midiAccess.outputs.values();
    
    for ( var output = outputs.next(); output && !output.done; output = outputs.next()){
        output = output.value;
    
        if (output.name.toString().indexOf("MIDI Port") != -1)
        {
            midiOut = output;
            break;
        }
    }
    
    if (midiOut && bassStationFound)
    {
        // whee!
        $('#device').text("Launchpad connected!");
    }
    else
    {
        $('#device').text("Please connect a Launchpad");
    }
}

function sendSysex(bytes)
{
    if (midiOut)
    {
        midiOut.send(bytes);
    }
}

function midiProc(event) {

        data = event.data;
        var cmd = data[0] >> 4;
        var channel = data[0] & 0xf;
        var noteNumber = data[1];
        var velocity = data[2];

}