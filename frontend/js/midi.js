window.addEventListener('load', function()
                        {
                            $('#device').text("MIDI loading...");
                            var opt = {sysex: true};
                            navigator.requestMIDIAccess(opt).then( onMIDIInit, onMidiError );
                        });

var midiAccess = null;
var midiIn = null;
var midiOut = null;
var bassStationFound = false;

function onMidiError( msg )
{
    console.log( "Failed to get MIDI access - " + msg );
    promptJazzPlugin();
}

function onMIDIInit( midi )
{
    midiAccess = midi;
    
    // inputs
    var list=midiAccess.inputs();
    
    for (var i=0; i<list.length; i++)
    {
        if (list[i].name.toString().indexOf("Bass Station") != -1) {
            
            midiIn = list[i];
            midiIn.onmidimessage = midiProc;
            bassStationFound = true;
        }
    }
    
    // output
    list=midi.outputs();
    for (var i=0; i<list.length; i++)
    {
        if (list[i].name.toString().indexOf("Bass Station") != -1)
        {
            midiOut = list[i];
            break;
        }
    }
    
    if (midiOut && bassStationFound)
    {
        // whee!
        $('#device').text("Bass Station II connected!");
    }
    else
    {
        $('#device').text("Please connect a Bass Station II");
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
    if (event.data.length == 154)
    {
        // got a patch!
        uploadSysexPatch(event.data);
    }
    else
    {
        data = event.data;
        var cmd = data[0] >> 4;
        var channel = data[0] & 0xf;
        var noteNumber = data[1];
        var velocity = data[2];
    }
}