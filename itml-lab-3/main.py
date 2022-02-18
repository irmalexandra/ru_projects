import pandas
import seaborn
import matplotlib.pyplot as pyplot
import sklearn.model_selection as model_selection
from sklearn.datasets import fetch_20newsgroups

newsgroups = fetch_20newsgroups(subset="all", remove=("headers", "footers", "quotes"))
base_dataframe = pandas.DataFrame(
    {"data": newsgroups["data"],
     "target": newsgroups["target"],
     "target_name": [newsgroups["target_names"][int(x)] for x in newsgroups["target"]]
     }
)

from sklearn.feature_extraction.text import CountVectorizer

vectorizer = CountVectorizer(max_df=0.5, min_df=3)

all_wordbag = vectorizer.fit_transform(base_dataframe["data"])

training_x_wordbag, test_x_wordbag, training_y, test_y = model_selection.train_test_split(all_wordbag,
                                                                                          base_dataframe["target_name"],
                                                                                          test_size=0.2)

topic_dict = dict()

# training_wordbag = vectorizer.fit_transform(training_x)

# original = 134410 => bad number, much bloat such repetetive, wow. <--- 3.a
# pruned =   134401
# removed  {'of': 5, 'the': 7, 'and': 0, 'to': 8, 'for': 1, 'is': 3, 'in': 2, 'that': 6, 'it': 4}
# vectorizer.vocabulary_.get("and")


# print(len(vectorizer.vocabulary_)) # <--- answer to 3.b


from sklearn import tree, metrics

decision_tree_classifier = tree.DecisionTreeClassifier()
decision_tree_classifier.fit(X=training_x_wordbag, y=training_y)  # <--- 4.a

print(decision_tree_classifier.tree_.node_count)  # <---- 4.b

#  ==================> 4.c <==================
print("training accuracy", metrics.accuracy_score(training_y, decision_tree_classifier.predict(training_x_wordbag)))

#  ==================> 4.d <==================
print("test accuracy", metrics.accuracy_score(test_y, decision_tree_classifier.predict(test_x_wordbag)))

#  We are seeing a moderately high amount of errors in the test data and a very low error rate in the training data,
#  which makes us suspect that the decision tree is overfitted

from sklearn.feature_selection import SelectKBest, chi2
feature_selector = SelectKBest(chi2, k=10)
training_bag = feature_selector.fit_transform(training_x_wordbag, training_y)
testing_bag = feature_selector.fit_transform(test_x_wordbag, test_y)


new_tree_too = tree.DecisionTreeClassifier().fit(X=training_bag, y=training_y)
print(tree.export_text(new_tree_too))

#  ==================> 5.c <==================
print("training accuracy", metrics.accuracy_score(training_y, new_tree_too.predict(training_bag)))
#  no

#  ==================> 5.b <==================
print("test accuracy", metrics.accuracy_score(test_y, new_tree_too.predict(testing_bag)))

#  For both 5.b and 5.c, it is not very effective to filter out so many features. Even though we leave the "top 10"
#  scoring features, the accuracy of our predictions plummets. This is most likely due to the importance of a large
#  dataset of words in order to clearly define which newsgroup each email belongs to.


#  We are seeing a severe amount of underfitting, which is not really surprising since we're decreasing our
#  features from around 35.000 down to just 10 <--- 5.a


ks_to_test = [35890, 35850, 32000]
for k in range(10, 35984, 100):
    feature_selector = SelectKBest(chi2, k=k)
    training_bag = feature_selector.fit_transform(training_x_wordbag, training_y)
    testing_bag = feature_selector.fit_transform(test_x_wordbag, test_y)
    new_tree_too = tree.DecisionTreeClassifier().fit(X=training_bag, y=training_y)

    print("for k is ", k)
    print("training accuracy", metrics.accuracy_score(training_y, new_tree_too.predict(training_bag)))

    print("test accuracy", metrics.accuracy_score(test_y, new_tree_too.predict(testing_bag)))


#  trying to find a good accuracy by changing the k is is not a good idea.