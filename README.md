# GuitarPro-to-Midi

run "dotnet build" to build console application

Features:
- Reading GuitarPro 3 - 5 Files (based on the open python pyGuitarPro project)
- Reading GuitarPro 6 Files (using a simple bitwise compression and an xml structure with dictionary and ids)
- (disabled) Reading GuitarPro 7 Files (packed like a normal zip-file and using a very large xml structure)
- Transferring all files into a common native format that saves all (and only) the information that are interesting for midi files. I.e. a lot of information like fingering or guitar amp preferences are ignored.
- Splitting to a secondary channel for certain effects
- Exporting to Midi, trying to simulate the sound as best as possible:
  Simulating:
    - Different types of harmonics
    - Strum patterns
    - Bending - as far as the midi standard allows
    - Trembar - "
    - Volume knob effects
    - Muted notes
    - Vibratos
    - and perhaps more..
 
 (I must mention that GuitarPro's native Midi export lacks far behind in this functionality!)
    
 Please enjoy and create some great software with this!
