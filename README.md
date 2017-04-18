# Retro Power Up's Lower Thirds Generator
This is the generator I use to create lower thirds for my YouTube channel, [Retro Power Up](https://www.youtube.com/retropowerup)

Example of a generated lower third:

![lt_bugsbunnycrazycastle3-italics](https://cloud.githubusercontent.com/assets/690781/24874400/7f43dd48-1df2-11e7-847b-335870378ac5.png)

### How to use

Run the executable `LowerThirdsGenerator.exe` with the following options:

**-r, -read**  
Input file to be processed  
Required

**-t, -template**  
Template vector file to be used  
Optional  
Defaults to lt_Template.svg

**-o, -overwrite**  
Overwrites existing SVG and PNG files  
Optional  
Defaults to false

**-v, -verbose**  
Prints all messages to standard output  
Optional  
Defaults to false

    LowerThirdsGenerator.exe -r input.txt -t lt_Template.svg -o True -v True

### Input file format

Each lower third consists of up to three lines of text.

The text file can have one to three lines of text per lower third, followed by an empty text line to separate each lower third

Example input:

    BUGS BUNNY: CRAZY CASTLE 3
    KEMCO/NINTENDO | JAN 1999
    PAID: $5
    
    RISK/BATTLESHIP/CLUE
    GRAVITY I/DESTINATION SOFTWARE | AUG 21, 2005
    PAID: $5
    
    MONSTER TRUCKS
    SKYWORKS TECHNOLOGIES/MAJESCO | NOV 11, 2004
    PAID: $5
    
    STAR WARS: FLIGHT OF THE FALCON
    POCKET STUDIOS/THQ | NOV 18, 2003
    PAID: $5
    
    SW TRILOGY: APPRENTICE OF THE FORCE
    UBISOFT | SEPT 24, 2004
    PAID: $5
    
    TOMB RAIDER III
    CORE DESIGN/EIDOS | NOV 20, 1998
    PAID: $1.50

    STREET FIGHTER EX PLUS ALPHA
    ARIKA/CAPCOM | SEPT 30, 1997
    PAID: $1.50

    SLED STORM
    EA | JULY 31, 1999
    PAID: $1.50

    TONY HAWK'S PRO SKATER
    NEVERSOFT/ACTIVISION | AUG 31, 1999
    PAID: $1.50

    RUSHDOWN
    CANAL+ MULTIMEDIA/EA | FEB 28, 1999
    PAID: $1.50

    NFL 2K1
    SEGA SPORTS | SEPT 7, 2000
    PAID: $1.50
    
You can also force empty lines by entering three dashes for a line. This special case is to handle if you want the first and/or second line to be empty lines, but not the second and/or third lines.

Example input: 

    FINAL FANTASY XV: COMPLETE OFFICIAL GUIDE
    ---
    PAID: $24
    
    ---
    ---
    PAID: $20
    
The program will warn you if it thinks one of the lines of text will be cut off if it is too long

### Template SVG file format

The only requirement of the SVG file is that it must have `[LINE1]`, `[LINE2]` and `[LINE3]` placeholders within the file in order for them to be replaced with values from the input file

    <text
        xml:space="preserve"
        style="font-style:normal;font-weight:normal;line-height:0%;font-family:sans-serif;letter-spacing:0px;word-spacing:0px;fill:#000000;fill-opacity:1;stroke:none"
        x="254.28572"
        y="898.07648"
        id="text3812">
            <tspan
                 sodipodi:role="line"
                 id="tspan3814"
                 x="254.28572"
                 y="898.07648"
                 style="font-style:italic;font-variant:normal;font-weight:normal;font-stretch:normal;font-size:80px;line-height:1.25;font-family:Lato;-inkscape-font-specification:Lato;fill:#ffffff">
                     [LINE ONE]
            </tspan>
    </text>
