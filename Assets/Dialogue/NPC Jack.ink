-> start

=== start ===
A long time ago, a couple of Skeletons stole my candy. 
It Would be nice if I could get it back. 
BY chance, have you come across my candy?
    * [Yes]
    -> success
    * [No]
    -> noCandy
-> END

=== noCandy ===
Come back if you find my candy
-> END

=== success ===
Thank you so much! Here's a reward.
-> END

=== failure ===
Look like you don't have it. Come back if
    you find my candy.
-> END

=== postCompletion ===
Thanks for helping me Earlier. Good luck
    With your adventures!
-> END