Litecoin-Dust-Settler
=====================

Support: https://forum.litecoin.net/index.php/topic,3870.0.html

Litecoin Dust Settler

Just wanted to lets you guys know about a program I created to takes and consolidate all those small p2pool payments into lump sums and do it without any fees if your blocks are old enough.

There is a mandatory 1.5% Developer donation fee for using this application.

Requirements:
1) Windows System (physical or VMware) with .NET 4 or higher
2) Linux System (physical or VMware)

Step by Step:

1) On your Linux system build litecoind 
              Fastest way : https://forum.litecoin.net/index.php/topic,43.0.html

2) Download the zip Litecoin Dust Settler 
              http://www.mediafire.com/download/kh3utrt67japtab/LitecoinDustSettler.zip

3) Install LiteCoin on Windows System

4) Copy your existing wallet.dat (the wallet with the litecoins you want to merge) file to the Linux systems ~/.litecoin directory

5) Edit ~/.litecoin/litecoin.conf file on the so that it looks like below:

server=1
rpcallowip=*
rpcuser=user
rpcpassword=password
rpcport=10333

6) start litecoind on the Linux system:

litecoind -disablesafemode -debug -daemon

7) start litecoin GUI on your windows system

8) Wait for both systems to sync up 100%

9) Start Litecoin Dust Settler.exe

10) Fill in the send to account field with a litecoin address from the windows litecoin client

11) Fill in the Wallet Password field with the password of the litecoin wallet in the Linux system

12) Fill in the address field like so:
http://[ip address of Linux system]:10333/

for example if my Linux system has a ip address of 192.168.13.143 this field would be

http://192.168.13.143:10333/

13) Fill in the RPC User field with "user" and RPC Password filed with "password"

14) Hit Test RPC, it should return a popup box with the version number and other info. If you get a error message make sure you check you installation and firewall settings

-------Once everything is setup you wont have to repeat steps 1-14-------------

15) Next hit "Get List" This will retrieve and sort automatically all your coins based on most probably for free transaction.

16) Now you should have a table of data in the bottom part of the screen use the shift key and the white box to the left of every row to select multiple rows that you want to combine, best to start out with the top 20 or so.

17) Next hit check selected. This will give you transaction estimates based on the selected rows, the only thing that matters here is the Free? label. The Free? Label tells you weather the transaction you select can be considered as free. If the label reads True then keep increasing the number of rows, until you get to a point where Free? results in False. Then select one less row so that it reads True. Basically you want to select as many rows as possible such that it results in True.

18) Next hit Create Transaction

19) Then hit Unlock Wallet after this step you must complete the next steps within 60 seconds or start over from Create Transaction

20) Hit Sign Transaction

21) Verify the Amount label and the dev donation current set at 1.5%, to support to this project.

22) Hit Send Transaction 

23) Finally lock Wallet
