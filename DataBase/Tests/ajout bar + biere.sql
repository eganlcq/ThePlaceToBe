/* Ajout nouveau bar */
call insertbar('testbar', 'testrue', 7, 1300, 'Wavre', 1);
call barfrominter(4, 'modiftestbar', 'modiftestrue', 6, 1300, 'Wavre', 12.1234567, 76.7654321, 1);

/* Ajout nouvelle biere dans ce nouveau bar*/
call insertscan('testbeer', 8.1, 'testtype', 'testbeer.jpg', 'modiftestbar');
call beerfromscan(83, 'modiftestbeer', 8.1, 'modiftesttype', 'testbeer.jpg', 'modiftestbar');

/* Ajout bière existante dans bar existant*/
call insertcarte('modiftestbeer', 'beer bar');
call cartefrominter(1, 'modiftestbeer', 'beer bar');