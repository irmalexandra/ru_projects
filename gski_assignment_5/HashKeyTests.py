import random
from MyHashableKey import MyHashableKey

new_key = MyHashableKey(28, '')
new_key = MyHashableKey(28, 'a')
hash(new_key)
new_key = MyHashableKey(28, 'a')
hash(new_key)
other_key = MyHashableKey(28, 'rikki')
print(hash(new_key))
print(hash(new_key))
print(hash(other_key))
print(hash(other_key))
print(new_key == other_key)
print(new_key == new_key)

NO_KEYS = 32
NO_BUCKETS = 8


def test_keys():
    spread_dict = {}
    for i in range(NO_KEYS):
        new_string = ""
        for j in range(10):
            new_string += chr(random.randint(0, 127))
        new_key = MyHashableKey(random.randint(1, 1000000), new_string)

        if hash(new_key) % NO_BUCKETS in spread_dict:
            spread_dict[hash(new_key) % NO_BUCKETS] += 1
        else:
            spread_dict[hash(new_key) % NO_BUCKETS] = 1

    for key, value in spread_dict.items():
        print("Hash Value: {}   Frequencey: {} Percentage of Keys: {:.2f}%".format(key, value, (value / NO_KEYS) * 100))

    print("Number of Buckets: {}".format(len(spread_dict)))
    print("Number of keys: {}".format(NO_KEYS))
    spread_list = sorted(spread_dict.values())
    print("Lowest value: {}".format(spread_list[0]))
    print("Highest value: {}".format(spread_list[-1]))
    difference = spread_list[-1] - spread_list[0]
    print("Difference: {}".format(difference))
    spread_percentage = 100 - 100 * ((difference) / NO_KEYS)
    print("Spread Percentage: {}".format(spread_percentage))
    return spread_percentage


success = 0
fails = 0
NO_TESTS = 1000
for i in range(NO_TESTS):
    spread_percentage = test_keys()
    if spread_percentage >= 80.0:
        success += 1
    else:
        fails += 1

print("Success: ", success)
print("Fails: ", fails)
print("Success ratio: ", success / NO_TESTS)
