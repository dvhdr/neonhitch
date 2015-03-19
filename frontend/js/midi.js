
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
    
    if ((typeof(midiAccess.inputs) == "function")) {  //Old Skool MIDI inputs() code
        
        console.log("browser wants old skool MIDI device access?")
    }
    else
    {
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
        
            if (output.name.toString().indexOf("Live Port") != -1)
            {
                midiOut = output;
                break;
            }
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
    
    onLaunchpad(data[0], data[1], data[2]);
    
}

function blit(pixels)
{
    if (midiOut)
    {
        var syx = [0xF0, 0x00, 0x20, 0x29, 0x02, 0x10, 0x0B];
        syx = syx.concat(pixels);
        syx = syx.concat(0xF7);
        
       // console.log(syx);
        
        midiOut.send(syx);
    }
}