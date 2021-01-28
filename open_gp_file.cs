using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;


    class Program
    {
    static GPFile gpfile;

    static void Main(string[] args)
    {
        if (args == null)
        {
            Console.WriteLine("Usage: GuitarPro-to-Midi filename");
            return;
        }

        if (args.Length != 1) {
            Console.WriteLine("Usage: GuitarPro-to-Midi filename");
            return;
        }
        string fileName = args[0];

        if (! File.Exists(fileName)){
            Console.WriteLine("ERROR: file"+fileName+" not found");
            return;
        }

        byte[] bytes = File.ReadAllBytes(fileName);
        //Detect Version by Filename
        int version = 7;
        string fileEnding = Path.GetExtension(fileName);
        if (fileEnding.Equals(".gp3")) version = 3;
        if (fileEnding.Equals(".gp4")) version = 4;
        if (fileEnding.Equals(".gp5")) version = 5;
        if (fileEnding.Equals(".gpx")) version = 6;
        if (fileEnding.Equals(".gp")) version = 7;


        switch (version)
        {
            case 3:
                gpfile = new GP3File(bytes);
                gpfile.readSong();
                break;
            case 4:
                gpfile = new GP4File(bytes);
                gpfile.readSong();
                break;
            case 5:
                gpfile = new GP5File(bytes);
                gpfile.readSong();
                
                break;
            case 6:
                gpfile = new GP6File(bytes);
                gpfile.readSong();
                gpfile = gpfile.self; //Replace with transferred GP5 file

                break;
/*            case 7:
                string archiveName = url.Substring(8).Replace("%20"," ");
                byte[] buffer = new byte[8200000];
                MemoryStream stream = new MemoryStream(buffer);
                using (var unzip = new Unzip(archiveName))
                {
                    //Console.WriteLine("Listing files in the archive:");
                    ListFiles(unzip);
                    
                    unzip.Extract("Content/score.gpif", stream);
                    stream.Position = 0;
                    var sr = new StreamReader(stream);
                    string gp7xml = sr.ReadToEnd();

                    gpfile = new GP7File(gp7xml);
                    gpfile.readSong();
                    gpfile = gpfile.self; //Replace with transferred GP5 file

                }
                break;*/
            default:
                Console.WriteLine("Unknown File Format");
                break;
        }
        Console.WriteLine("Done");

        var song = new Native.NativeFormat(gpfile);
        var midi = song.toMidi();
        List<byte> data = midi.createBytes();
        var dataArray = data.ToArray();
        using (var fs = new FileStream("output.mid", FileMode.OpenOrCreate, FileAccess.ReadWrite))
        {
            fs.Write(dataArray, 0, dataArray.Length);
            
        }
        //Create Example Track (delete)
        /*
         MidiExport.MidiExport midi = new MidiExport.MidiExport();
        midi.fileType = 2;
        midi.ticksPerBeat = 960;

        MidiExport.MidiTrack track = new MidiExport.MidiTrack();
        track.messages.Add(new MidiExport.MidiMessage("track_name",new string[] { "untitled" },0));
        track.messages.Add(new MidiExport.MidiMessage("time_signature", new string[] {"4","4","24","8" }, 0));
        track.messages.Add(new MidiExport.MidiMessage("control_change", new string[] { "0","7", "100" }, 0));
        track.messages.Add(new MidiExport.MidiMessage("program_change", new string[] { "0", "48" }, 0));
        track.messages.Add(new MidiExport.MidiMessage("note_on", new string[] { "0", "55","91" }, 0));
        track.messages.Add(new MidiExport.MidiMessage("note_on", new string[] { "0", "55", "0" }, 256));
        track.messages.Add(new MidiExport.MidiMessage("end_of_track", new string[] { }, 0));
        midi.midiTracks.Add(track);
        midi.createBytes();
         */

    }
}
