﻿using System;
using System.Collections.Generic;
using BrunoMikoski.AnimationSequencer.Utility;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace BrunoMikoski.AnimationSequencer
{
    public static class DOTweenActionEditorGUIUtility
    {
        private static Dictionary<Type, GUIContent> cachedTypeToDisplayName;
        public static Dictionary<Type, GUIContent> TypeToDisplayName
        {
            get
            {
                CacheDisplayTypes();
                return cachedTypeToDisplayName;
            }
        }
        
        private static Dictionary<Type, GUIContent> cachedTypeToInstance;
        public static Dictionary<Type, GUIContent> TypeToParentDisplay
        {
            get
            {
                CacheDisplayTypes();
                return cachedTypeToInstance;
            }
        }

        
        private static Dictionary<Type, DOTweenActionBase> typeToInstanceCache;
        public static Dictionary<Type, DOTweenActionBase> TypeToInstanceCache
        {
            get
            {
                CacheDisplayTypes();
                return typeToInstanceCache;
            }
        }
        
        private static DOTweenActionsAdvancedDropdown cachedDOTweenActionsDropdown;
        public static DOTweenActionsAdvancedDropdown DOTweenActionsDropdown
        {
            get
            {
                if (cachedDOTweenActionsDropdown == null)
                    cachedDOTweenActionsDropdown = new DOTweenActionsAdvancedDropdown(new AdvancedDropdownState());
                return cachedDOTweenActionsDropdown;
            }
        }
        

        public static GUIContent GetTypeDisplayName(Type targetBaseDOTweenType)
        {
            if (TypeToDisplayName.TryGetValue(targetBaseDOTweenType, out GUIContent result))
                return result;

            return new GUIContent(targetBaseDOTweenType.Name);
        }

        private static void CacheDisplayTypes()
        {
            if (cachedTypeToDisplayName != null)
                return;

            cachedTypeToDisplayName = new Dictionary<Type, GUIContent>();
            cachedTypeToInstance = new Dictionary<Type, GUIContent>();
            typeToInstanceCache = new Dictionary<Type, DOTweenActionBase>();
            
            List<Type> types = TypeUtility.GetAllSubclasses(typeof(DOTweenActionBase));
            for (int i = 0; i < types.Count; i++)
            {
                Type type = types[i];
                if (type.IsAbstract)
                    continue;
                
                DOTweenActionBase doTweenActionBaseInstance = Activator.CreateInstance(type) as DOTweenActionBase;
                if (doTweenActionBaseInstance == null)
                    continue;
                GUIContent guiContent = new GUIContent(doTweenActionBaseInstance.DisplayName);
                if (doTweenActionBaseInstance.TargetComponentType != null)
                {
                    GUIContent targetComponentGUIContent = EditorGUIUtility.ObjectContent(null, doTweenActionBaseInstance.TargetComponentType);
                    guiContent.image = targetComponentGUIContent.image;
                    GUIContent parentGUIContent = new GUIContent(doTweenActionBaseInstance.TargetComponentType.Name)
                    {
                        image = targetComponentGUIContent.image
                    };
                    cachedTypeToInstance.Add(type, parentGUIContent);
                }
                
                cachedTypeToDisplayName.Add(type, guiContent);
                typeToInstanceCache.Add(type, doTweenActionBaseInstance);
            }
        }
        
        public static bool CanActionBeAppliedToTarget(Type targetActionType, GameObject targetGameObject)
        {
            if (targetGameObject == null)
                return false;

            if (TypeToInstanceCache.TryGetValue(targetActionType, out DOTweenActionBase actionBaseInstance))
            {
                Type requiredComponent = actionBaseInstance.TargetComponentType;
                
                if (requiredComponent == typeof(Transform))
                    return true;
                    
                if (requiredComponent == typeof(RectTransform))
                    return targetGameObject.transform is RectTransform;

                return targetGameObject.GetComponent(requiredComponent) != null;
            }
            return false;
        }
    }
}
